using System.Collections;
using System.Diagnostics;
using System.Reflection;

public partial class clsInsert
{
    private readonly ArrayList fields = new();
    private readonly ArrayList values = new();
    private string tableName = "";

    public clsInsert()
    {
        fields.Clear();
        values.Clear();
        tableName = string.Empty;
    }

    public void Init(string pTableName)
    {
        fields.Clear();
        values.Clear();
        tableName = pTableName.Trim();
    }

    public void Add(string pField, object pValue, string pTipo)
    {
        try
        {
            if (string.IsNullOrEmpty(pField) || string.IsNullOrEmpty(pTipo))
                return;

            fields.Add(pField);
            values.Add(pValue);
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethod = sf?.GetMethod();
            throw new Exception($"{currentMethod} {ex.Message}");
        }
    }

    public string SQL()
    {
        if (string.IsNullOrEmpty(tableName) || fields.Count == 0 || values.Count == 0 || fields.Count != values.Count)
            return string.Empty;

        try
        {
            var fieldList = string.Join(", ", fields.ToArray());
            var valueList = string.Join(", ", values.ToArray());
            return $"INSERT INTO {tableName} ({fieldList}) VALUES ({valueList})";
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethod = sf?.GetMethod();
            throw new Exception($"{currentMethod} {ex.Message}");
        }
    }
}