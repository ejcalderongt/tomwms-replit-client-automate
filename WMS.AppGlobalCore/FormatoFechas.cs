using Microsoft.VisualBasic;
public class FormatoFechas
{
    public static string fFecha(DateTime pFecha, bool Con_Hora = false)
    {

        string vfFecha = "";

        try
        {

            if (Information.IsDate(pFecha))
            {
                if (Con_Hora)
                {
                    string Hora = string.Format("{0}:{1}:{2}", pFecha.Hour, Strings.Right("00" + pFecha.Minute, 2), Strings.Right("00" + pFecha.Second, 2));
                    vfFecha = string.Format("'{0}{1}{2} {3}'", pFecha.Year.ToString(), Strings.Right("00" + pFecha.Month, 2), Strings.Right("00" + pFecha.Day, 2), Hora);
                }
                else
                    vfFecha = string.Format("'{0}{1}{2}'", pFecha.Year.ToString(), Strings.Right("00" + pFecha.Month, 2), Strings.Right("00" + pFecha.Day, 2));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return vfFecha;
    }

    public static string tFecha(DateTime pFecha)
    {
        string vfFecha = "";
        try
        {
            if (Information.IsDate(pFecha))
                vfFecha = Convert.ToString(pFecha.Year) + Strings.Right("00" + pFecha.Month, 2) + Strings.Right("00" + pFecha.Day, 2);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return vfFecha;
    }

    public static string sFecha(DateTime pFecha)
    {
        string vfFecha = "";
        try
        {
            if (Information.IsDate(pFecha))
                vfFecha = string.Format("{0}/{1}/{2}", Strings.Right(string.Format("00{0}", pFecha.Day), 2), Strings.Right("00" + pFecha.Month, 2), pFecha.Year.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return vfFecha;
    }

    public static string sFechaM(DateTime pFecha)
    {
        string vfFecha = "";
        try
        {
            if (Information.IsDate(pFecha))
                vfFecha = string.Format("{0}/{1}/{2}", Strings.Right(string.Format("00{0}", pFecha.Month), 2), Strings.Right("00" + pFecha.Day, 2), pFecha.Year.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return vfFecha;
    }

    public static string sHora(DateTime pFecha)
    {
        string vsHora = "";
        try
        {
            if (Information.IsDate(pFecha))
                vsHora = string.Format("{0}:{1}:{2}", Strings.Right("00" + pFecha.Hour, 2), Strings.Right("00" + pFecha.Minute, 2), pFecha.Second.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return vsHora;
    }

    public static string fFechaHora(DateTime pFecha)
    {
        string vfFecha = "'01-Jan-1900 00:00:00'";
        if (Information.IsDate(pFecha))
        {
            vfFecha = string.Format("'{0}{1}{2} ", pFecha.Year.ToString(), Strings.Right("00" + pFecha.Month, 2), Strings.Right("00" + pFecha.Day, 2));
            vfFecha = string.Format("{0}{1}:{2}:{3}'", vfFecha, pFecha.Hour.ToString(), pFecha.Minute.ToString(), pFecha.Second.ToString());
        }
        return vfFecha;
    }

    public static string fFechaHora_Time(DateTime pFecha)
    {
        string vfFecha = "'01-Jan-1900 00:00:00'";
        if (Information.IsDate(pFecha))
        {
            vfFecha = string.Format("'{0}{1}{2} ", pFecha.Year.ToString(), Strings.Right("00" + pFecha.Month, 2), Strings.Right("00" + pFecha.Day, 2));
            vfFecha = string.Format("{0}{1}:{2}:{3}'", vfFecha, pFecha.Hour.ToString(), pFecha.Minute.ToString(), pFecha.Second.ToString());
        }
        return vfFecha;
    }

    public static DateTime sFecha_To_Date(string pFecha)
    {
        DateTime vsFecha_To_Date = new DateTime(1900, 1, 1);
        try
        {

            int vAño = int.Parse(pFecha.Substring(0, 4).ToString());
            int vMes = int.Parse(pFecha.Substring(4, 2).ToString());
            int vDia = int.Parse(pFecha.Substring(6, 2).ToString());

            vsFecha_To_Date = new DateTime(vAño, vMes, vDia);
        }
        catch (Exception)
        {
            throw;
        }
        return vsFecha_To_Date;
    }
}