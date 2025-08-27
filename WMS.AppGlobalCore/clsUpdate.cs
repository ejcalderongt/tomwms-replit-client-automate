using System.Collections;
using System.Diagnostics;
using System.Reflection;

public partial class clsUpdate
{
    private ArrayList clFList = new ArrayList();
    private string clTable;
    private string vWhere = "";

    public clsUpdate()
    {
        clFList.Clear();
        clTable = "";
    }

    public void Init(string TableName)
    {
        clFList.Clear();
        clTable = string.Format("UPDATE {0} SET ", TableName);
    }

    public void Add(string pField, object pValue, string pTipo)
    {
        try
        {
            if (string.IsNullOrEmpty(pField) || string.IsNullOrEmpty(pTipo))
                return;

            string assignment = string.Format("{0} = {1}", pField, pValue);
            clFList.Add(assignment);
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            throw new Exception(string.Format("{0} {1}", currentMethodName, ex.Message));
        }
    }

    public void Where(string pWhere)
    {
        vWhere = " WHERE " + pWhere;
    }

    public string SQL()
    {
        if (string.IsNullOrEmpty(clTable) || clFList.Count == 0)
            return string.Empty;

        try
        {
            string assignments = string.Join(", ", clFList.ToArray());
            return clTable + assignments + vWhere;
        }
        catch (Exception ex)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            MethodBase? currentMethodName = sf?.GetMethod();
            throw new Exception(string.Format("{0} {1}", currentMethodName, ex.Message));
        }
    }
}
