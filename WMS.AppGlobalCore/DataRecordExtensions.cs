using System.Data;
public static class DataRecordExtensions
{
    public static DataRow ToDataRow(this IDataRecord record)
    {
        var table = new DataTable();
        for (int i = 0; i < record.FieldCount; i++)
            table.Columns.Add(record.GetName(i), record.GetFieldType(i));

        var row = table.NewRow();
        for (int i = 0; i < record.FieldCount; i++)
            row[i] = record.IsDBNull(i) ? DBNull.Value : record.GetValue(i);

        table.Rows.Add(row);
        return row;
    }
}