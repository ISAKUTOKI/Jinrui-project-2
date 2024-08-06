//The MIT License (MIT)

//Copyright( c) 2016
//Tabasco Interactive

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace com
{
    public class XLSDatabase
    {

        public delegate void ItemCallback( SerializedProperty property, Dictionary<string, object> rowDict);

        /// <summary>
        /// Imports data from a spreadsheet to SerializedProperty
        /// </summary>
        /// <param name="filePath">path to spreadsheet file</param>
        /// <param name="sheetName">name of the sheet from which the data will be pulled</param>
        /// <param name="property">The serialized property to which the data will be loaded</param>
        /// <param name="append">If true - the data will be appended to the property; otherwise the data will be overwritten</param>
        /// <param name="itemCallback">Optional callback for each row - use it for setting up custom fields</param>
        public static void ImportList( string filePath, string sheetName, SerializedProperty property, bool append = false, ItemCallback itemCallback = null )
        {
            ImportList( filePath, sheetName, property, 0, append, itemCallback );
        }

        /// <summary>
        /// Imports data from a spreadsheet to SerializedProperty
        /// </summary>
        /// <param name="filePath">path to spreadsheet file</param>
        /// <param name="sheetName">name of the sheet from which the data will be pulled</param>
        /// <param name="property">The serialized property to which the data will be loaded</param>
        /// <param name="append">If true - the data will be appended to the property; otherwise the data will be overwritten</param>
        /// <param name="itemCallback">Optional callback for each row - use it for setting up custom fields</param>
        public static void ImportList( string filePath, string sheetName, SerializedProperty property, int firstRowIdx, bool append = false, ItemCallback itemCallback = null )
        {
            ReadSheet( filePath, sheetName, firstRowIdx, ( sheet, firstRow, formulaEvaluator ) => {
                if ( !property.isArray ) {
                    Debug.LogError( "Property should be an array type" );
                    return;
                }
                List<string> names = new List<string>();
                foreach ( ICell cell in firstRow ) {
                    names.Add( cell.StringCellValue );
                }
                if ( !append ) {
                    property.arraySize = 0;
                }
                for ( int rownum = 1, i = append ? property.arraySize : 0; rownum <= sheet.LastRowNum; rownum++ ) {
                    IRow row = sheet.GetRow( rownum );
                    if ( row == null ) continue;
                    property.arraySize++;
                    SerializedProperty item = property.GetArrayElementAtIndex( i );
                    foreach ( SerializedProperty field in item ) {
                        int cellnum = names.FindIndex( ( x ) => x == item.name );
                        if ( cellnum >= 0 ) {
                            SetProperty( row.GetCell( cellnum, MissingCellPolicy.CREATE_NULL_AS_BLANK ), field );
                        }
                    }
                    if ( itemCallback != null ) {
                        itemCallback.Invoke( property.GetArrayElementAtIndex( i ), ExtractRowDict( row, firstRow, formulaEvaluator ) );
                    }
                    i++;
                }
            } );
        }

        public static void ImportMultiList( string filePath, string sheetName, SerializedProperty property, int firstRowIdx = 0 )
        {
            ReadSheet( filePath, sheetName, firstRowIdx, ( sheet, firstRow, formulaEvaluator ) => {
                if ( !property.isArray ) {
                    Debug.LogError( "Property should be an array type" );
                    return;
                }
                property.arraySize = firstRow.LastCellNum - 1;
                for ( int i = firstRowIdx; i <= sheet.LastRowNum; i++ ) {
                    IRow row = sheet.GetRow( i );
                    if ( row == null ) {
                        continue;
                    }
                    string cellValue = row.GetCell( 0, MissingCellPolicy.CREATE_NULL_AS_BLANK ).StringCellValue;
                    if ( !string.IsNullOrEmpty( cellValue ) ) {
                        if ( cellValue.EndsWith( "[]" ) ) {
                            i = ExtractArray( sheet, property, cellValue.Substring( 0, cellValue.Length - 2 ), i + 1, firstRow.LastCellNum );
                        } else {
                            // a regular property field
                            for ( int j = 1; j < firstRow.LastCellNum; j++ ) {
                                SerializedProperty field = property.GetArrayElementAtIndex( j - 1 ).FindPropertyRelative( cellValue );
                                if ( field != null ) {
                                    SetProperty( row.GetCell( j, MissingCellPolicy.CREATE_NULL_AS_BLANK ), field );
                                }
                            }
                        }
                    }
                }
            } );
        }

        /// <summary>
        /// Imports data from a spreadsheet to SerializedProperty
        /// </summary>
        /// <param name="filePath">path to spreadsheet file</param>
        /// <param name="sheetName">name of the sheet from which the data will be pulled</param>
        /// <param name="firstRowIdx">Index of the first row with data</param>
        /// <returns>A list with row, each row is a dictionary where key -> field name, value -> field value</returns>
        public static List<Dictionary<string, object>> ImportList( string filePath, string sheetName, int firstRowIdx = 0 )
        {
            var result = new List<Dictionary<string, object>>();
            ReadSheet( filePath, sheetName, firstRowIdx, ( sheet, firstRow, formulaEvaluator ) => {
                for ( int i = 1; i <= sheet.LastRowNum; i++ ) {
                    IRow row = sheet.GetRow( i );
                    if ( row == null ) {
                        continue;
                    }
                    var rowDict = ExtractRowDict( row, firstRow, formulaEvaluator );
                    result.Add( rowDict );
                }
            } );
            return result;
        }

        public static void ImportMultiList( string filePath, SerializedProperty property )
        {
            ImportMultiList( filePath, property.name, property );
        }

        public static void ImportList( string filePath, SerializedProperty property, bool append = false )
        {
            ImportList( filePath, property.name, property, append );
        }

        public static void ExportList( string filePath, string sheetName, List<Dictionary<string, object>> rowDict )
        {
            if ( rowDict.Count < 1 ) return;
            WriteSheet( filePath, sheetName, ( sheet, firstRow ) => {
                var keys = rowDict[0].Keys;
                // print header
                for ( int i = 0; i < keys.Count; i++ ) {
                    ICell nameCell = firstRow.GetCell( i, MissingCellPolicy.CREATE_NULL_AS_BLANK );
                    nameCell.SetCellValue( keys.ElementAt( i ) );
                }
                for ( int i = 0, rowNum = 1; i < rowDict.Count; i++, rowNum++ ) {
                    var row = GetOrCreateRow(sheet, rowNum);
                    for (int j = 0; j < keys.Count; j++ ) {
                        var cell = row.GetCell(j, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                        SetCellValue( cell, rowDict[i][keys.ElementAt( j ) ] );
                    }
                }
            } );
        }

        public static void ExportList( string filePath, SerializedProperty property )
        {
            ExportList( filePath, property.name, property );
        }

        public static void ExportList( string filePath, string sheetName, SerializedProperty property )
        {
            WriteSheet( filePath, sheetName, ( sheet, firstRow ) => {
                if ( !property.isArray ) {
                    Debug.LogError( "Property should be an array type" );
                    return;
                }
                if ( property.arraySize <= 0 ) {
                    Debug.LogError( "Empty array - nothing to export" );
                    return;
                }
                SerializedProperty item = property.GetArrayElementAtIndex( 0 );
                int i = 0;
                foreach ( SerializedProperty p in item ) {
                    ICell nameCell = firstRow.GetCell( i, MissingCellPolicy.CREATE_NULL_AS_BLANK );
                    nameCell.SetCellValue( p.name );
                    Debug.Log( p.name );
                    i++;
                }
                int rownum = 1;
                foreach ( SerializedProperty p in property ) {
                    int cellnum = 0;
                    IRow row = GetOrCreateRow( sheet, rownum );
                    foreach ( SerializedProperty field in p ) {
                        ICell cell = row.GetCell( cellnum, MissingCellPolicy.CREATE_NULL_AS_BLANK );
                        SetCellValue( cell, field );
                        cellnum++;
                    }
                    rownum++;
                }
            } );
        }

        public static string FindSpreadSheetByName( string fileName )
        {
            string[] assets = AssetDatabase.FindAssets( fileName );
            for ( int i = 0; i < assets.Length; i++ ) {
                string filePath = AssetDatabase.GUIDToAssetPath( assets[i] );
                if ( filePath.EndsWith( ".xls" ) || filePath.EndsWith( ".xlsx" ) ) {
                    return filePath;
                }
            }
            return null;
        }

        public static void MapRow(Dictionary<string, object> row, SerializedObject serilizedObject)
        {
            foreach(var keyVal in row ) {
                var property = serilizedObject.FindProperty(keyVal.Key);
                if ( property == null )
                    continue;
                switch( property.propertyType ){
                    case SerializedPropertyType.Boolean: property.boolValue = (bool)keyVal.Value; break;
                    case SerializedPropertyType.Enum:
                        property.enumValueIndex = ArrayUtility.FindIndex( property.enumNames, name => name == (string)keyVal.Value );
                        break;
                    case SerializedPropertyType.Float:      property.floatValue = System.Convert.ToSingle( keyVal.Value ); break;
                    case SerializedPropertyType.Integer:    property.intValue = System.Convert.ToInt32( keyVal.Value ); break;
                    case SerializedPropertyType.String:     property.stringValue = ( string )keyVal.Value; break;
                }
            }
        }

        protected static void WriteSheet( string filePath, string sheetName, System.Action<ISheet, IRow> callback )
        {
            IWorkbook workbook;
            if ( File.Exists( filePath ) ) {
                using ( Stream stream = new FileStream( filePath, FileMode.OpenOrCreate, FileAccess.Read ) ) {
                    workbook = WorkbookFactory.Create( stream );
                }
            } else {
                workbook = new HSSFWorkbook();
            }
            ISheet sheet = GetOrCreateSheet( workbook, sheetName );
            IRow firstRow = GetOrCreateRow( sheet, 0 );
            callback( sheet, firstRow );
            using ( Stream stream = new FileStream( filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite ) ) {
                workbook.Write( stream );
            }
        }

        protected static void ReadSheet( string filePath, string sheetName, int firstRowIdx, System.Action<ISheet, IRow, IFormulaEvaluator> callback )
        {
            using ( Stream stream = new FileStream( filePath, FileMode.OpenOrCreate, FileAccess.Read ) ) {
                var workbook = WorkbookFactory.Create( stream );
                var formulaEvaluator = WorkbookFactory.CreateFormulaEvaluator( workbook );
                ISheet sheet = GetOrCreateSheet( workbook, sheetName );
                IRow firstRow = sheet.GetRow( firstRowIdx );
                if ( firstRow == null ) {
                    Debug.LogError( "Sheet " + sheetName + " is empty, aborting." );
                    return;
                }
                callback( sheet, firstRow, formulaEvaluator );
            }
        }

        protected static int ExtractArray( ISheet sheet, SerializedProperty property, string arrayName, int startingRow, int cellNumber )
        {
            int i = startingRow;
            for ( ; i <= sheet.LastRowNum; i++ ) {
                IRow row = sheet.GetRow( i );
                ICell firstCell = row.GetCell( 0, MissingCellPolicy.RETURN_BLANK_AS_NULL );
                if ( firstCell == null )
                    break;
                for ( int j = 1; j < cellNumber; j++ ) {
                    ICell cell = row.GetCell( j, MissingCellPolicy.RETURN_BLANK_AS_NULL );
                    if ( cell != null ) {
                        SerializedProperty p = property.GetArrayElementAtIndex( j - 1 );
                        SerializedProperty arrayProp = p.FindPropertyRelative( arrayName );
                        arrayProp.arraySize = i - startingRow + 1;
                        SetProperty( cell, arrayProp.GetArrayElementAtIndex( i - startingRow ) );
                    }
                }
            }
            return i;
        }

        protected static object ExtractObject( ICell cell, IFormulaEvaluator formulaEvaluator )
        {
            if ( cell == null ) {
                return null;
            }
            switch ( cell.CellType ) {
                case CellType.Boolean: return cell.BooleanCellValue;
                case CellType.Numeric: return cell.NumericCellValue;
                case CellType.String: return cell.StringCellValue;
                case CellType.Formula:
                    try {
                        return ExtractCellValue( formulaEvaluator.Evaluate( cell ) );
                    }catch(System.IndexOutOfRangeException e ) {
                        Debug.LogError( cell.RowIndex.ToString() + "," + cell.ColumnIndex + " "+  cell.CellFormula );
                        throw e;
                    }
            }
            return null;
        }
        

        protected static object ExtractCellValue( CellValue cell )
        {
            if ( cell == null ) {
                return null;
            }
            switch ( cell.CellType ) {
                case CellType.Boolean: return cell.BooleanValue;
                case CellType.Numeric: return cell.NumberValue;
                case CellType.String: return cell.StringValue;
            }
            return null;
        }

        protected static Dictionary<string, object> ExtractRowDict( IRow row, IRow firstRow, IFormulaEvaluator formulaEvaluator )
        {
            var rowDict = new Dictionary<string, object>();
            for ( int j = 0; j < row.LastCellNum; j++ ) {
                var header = firstRow.GetCell( j, MissingCellPolicy.RETURN_BLANK_AS_NULL );
                if ( header == null )
                    continue;
                rowDict[header.StringCellValue] = ExtractObject( row.GetCell( j ), formulaEvaluator );
            }
            return rowDict;
        }

        protected static IRow GetOrCreateRow( ISheet sheet, int rownum )
        {
            IRow row = sheet.GetRow( rownum );
            if ( row == null ) {
                row = sheet.CreateRow( rownum );
            }
            return row;
        }

        protected static ISheet GetOrCreateSheet( IWorkbook workbook, string sheetName )
        {
            ISheet sheet = workbook.GetSheet( sheetName );
            if ( sheet == null ) {
                sheet = workbook.CreateSheet( sheetName );
            }
            return sheet;
        }

        protected static void SetCellValue(ICell cell, object value)
        {
            if (value is bool ) {
                cell.SetCellValue( (bool) value );
            }
            else if (value is int || value is float || value is double ) {
                cell.SetCellValue( System.Convert.ToDouble( value ) );
            }
            else if ( value is Object && AssetDatabase.Contains(value as Object ) ) {
                cell.SetCellValue( AssetDatabase.GetAssetPath( value as Object ) );
            } else {
                cell.SetCellValue( value.ToString() );
            }
        }

        protected static void SetCellValue( ICell cell, SerializedProperty property )
        {
            switch ( property.propertyType ) {
                case SerializedPropertyType.Boolean:    cell.SetCellValue( property.boolValue ); break;
                case SerializedPropertyType.Enum:       cell.SetCellValue( property.enumNames[property.enumValueIndex] ); break;
                case SerializedPropertyType.Float:      cell.SetCellValue( property.floatValue ); break;
                case SerializedPropertyType.Integer:    cell.SetCellValue( property.intValue ); break;
                case SerializedPropertyType.String:     cell.SetCellValue( property.stringValue ); break;
                case SerializedPropertyType.ObjectReference:
                    if ( AssetDatabase.Contains( property.objectReferenceValue ) ) {
                        cell.SetCellValue( AssetDatabase.GetAssetPath( property.objectReferenceValue ) );
                        if ( !cell.StringCellValue.Contains( ".prefab" ) ) {
                            Debug.LogError( "Currently only prefabs are serialized" );
                        }
                    } else {
                        Debug.LogError( "Can serialize only references to assets, not scene or runtime objects" );
                    }
                    break;
                default:
                    Debug.LogError( "Unsupported property type: " + property.propertyType );
                    break;
            }
        }

        protected static void SetProperty( ICell cell, SerializedProperty property )
        {
            DataFormatter df = new DataFormatter();
            switch ( property.propertyType ) {
                case SerializedPropertyType.Boolean: property.boolValue = cell.BooleanCellValue; break;
                case SerializedPropertyType.Enum:
                    int enumIndex = ( new List<string>( property.enumNames ) ).FindIndex( ( x ) => x == cell.StringCellValue );
                    property.enumValueIndex = enumIndex;
                    if ( enumIndex < 0 ) {
                        Debug.LogError( "Could not match enum value for property : " + property.propertyPath + ", cell value: " + cell.StringCellValue );
                    }
                    break;
                case SerializedPropertyType.Float:      property.floatValue  = ( float )cell.NumericCellValue; break;
                case SerializedPropertyType.Integer:    property.intValue    = ( int )cell.NumericCellValue; break;
                case SerializedPropertyType.String:     property.stringValue = df.FormatCellValue(cell); break;
                case SerializedPropertyType.Generic:
                    if ( property.isArray ) {
                        string[] values = cell.StringCellValue.Split( ',' );
                        property.arraySize = values.Length;
                        for ( int i = 0; i < values.Length; i++ ) {
                            SerializedProperty element = property.GetArrayElementAtIndex( i );
                            if ( element.propertyType != SerializedPropertyType.String ) {
                                Debug.LogError( "Unsupported property " + property.name + " type : Array of " + element.propertyType );
                                return;
                            }
                            element.stringValue = values[i];
                        }
                    } else {
                        Debug.LogError( "Unsupported property type: " + property.propertyType );
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    if ( !cell.StringCellValue.Contains( ".prefab" ) ) {
                        Debug.LogError( "Currently only prefabs are serialized" );
                    }
                    property.objectReferenceValue = AssetDatabase.LoadAssetAtPath( cell.StringCellValue, typeof( GameObject ) );
                    break;
                default:
                    Debug.LogError( "Unsupported property type: " + property.propertyType );
                    break;
            }
        }
    }
}
