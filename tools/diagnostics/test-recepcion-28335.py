"""#CKFK20261117_REC_CM: execute production snippets with in-memory fixtures only."""
from pathlib import Path
import subprocess
import json

root = Path(__file__).resolve().parents[2]
out = root / 'analysis/20261117-recepcion-28335/tests'
out.mkdir(exist_ok=True)
hh = Path('C:/Users/carol/StudioProjects/TOMHH2025/app/src/main/java')
bof = Path('C:/Users/carol/source/repos/TOMWMS_BOF')
screen = (hh / 'com/dts/tom/Transacciones/Recepcion/frm_list_rec_prod.java').read_text('utf-8')
start = screen.index('                List<clsBeTrans_oc_det_lote> lotesPendientes =')
end = screen.index('                ListaDetLotesOc.items = lotesPendientes;', start)
loop = screen[start:end]
entity = (hh / 'com/dts/classes/Transacciones/OrdenCompra/Trans_oc_det/clsBeTrans_oc_det.java').read_text('utf-8')
clone_start = entity.index('    @Override\n    public clsBeTrans_oc_det clone()')
clone_end = entity.index('\n    }', clone_start) + 6
clone = entity[clone_start:clone_end]
java = r'''
import java.util.*;
import java.util.function.Predicate;
class TestCajaMaster {
  static class clsBeTrans_oc_det implements Cloneable {
    int IdOrdenCompraEnc=28359, IdOrdenCompraDet=2, IdProductoBodega=1431878, No_Linea=1, IdProductoTallaColor=40159;
    double Cantidad=7, Cantidad_recibida=0;
    CLONE
  }
  static class clsBeTrans_oc_det_lote extends clsBeTrans_oc_det { int No_linea=1; }
  static class L<T>{ List<T> items=new ArrayList<>(); }
  static class Seq<T>{ List<T> xs; Seq(List<T> x){xs=x;} Seq<T> where(Predicate<T> p){return new Seq<>(xs.stream().filter(p).toList());} T firstOrNull(){return xs.isEmpty()?null:xs.get(0);} }
  static <T> Seq<T> stream(List<T> x){return new Seq<>(x);}
  static L<clsBeTrans_oc_det> pListDetalleOC;
  static L<clsBeTrans_oc_det_lote> ListaDetLotesOc;
  static List<clsBeTrans_oc_det> ListaFiltradaOc;
  static class GL { boolean Control_Talla_Color=true; }
  static GL gl=new GL();
  static List<clsBeTrans_oc_det_lote> select() throws Exception {
    LOOP
    return lotesPendientes;
  }
  static void check(boolean ok,String s){if(!ok)throw new AssertionError(s);}
  static void setup(double rec,double... qs){
    pListDetalleOC=new L<>(); clsBeTrans_oc_det oc=new clsBeTrans_oc_det();oc.Cantidad_recibida=rec;pListDetalleOC.items.add(oc);
    ListaDetLotesOc=new L<>(); ListaFiltradaOc=new ArrayList<>();
    for(double q:qs){var l=new clsBeTrans_oc_det_lote();l.Cantidad=q;ListaDetLotesOc.items.add(l);}
  }
  public static void main(String[] a)throws Exception{
    for(double[] q:new double[][]{{4,3},{3,4}}){
      setup(0,q);select();
      check(ListaFiltradaOc.size()==2,"two rows");
      check(ListaFiltradaOc.get(0)!=ListaFiltradaOc.get(1),"independent rows");
      check(ListaFiltradaOc.get(0)!=pListDetalleOC.items.get(0),"not OC reference");
      check(ListaFiltradaOc.get(0).Cantidad==q[0]&&ListaFiltradaOc.get(1).Cantidad==q[1],"4+3 in either order");
      check(pListDetalleOC.items.get(0).Cantidad==7&&pListDetalleOC.items.get(0).Cantidad_recibida==0,"original unchanged");
    }
    setup(6,4,3);ListaDetLotesOc.items.get(0).Cantidad_recibida=3;ListaDetLotesOc.items.get(1).Cantidad_recibida=3;
    check(select().size()==1&&ListaFiltradaOc.get(0).Cantidad==1,"recovery: only 1 pending");
    setup(7,4,3);check(select().isEmpty(),"completed OC excluded despite legacy lot counters");
    setup(0,4,3);ListaDetLotesOc.items.get(1).IdProductoTallaColor=40160;
    try{select();throw new AssertionError("wrong variant accepted");}catch(IllegalStateException expected){}
    System.out.println("PASS HH: order independence, references, immutable OC, recovery saldo=1, completed OC, variant mismatch");
  }
}
'''.replace('CLONE', clone).replace('LOOP', loop)
(out / 'TestCajaMaster.java').write_text(java, 'utf-8')
subprocess.run(['java', str(out / 'TestCajaMaster.java')], check=True)

code = (bof / 'TOMIMSV4/DAL/Transacciones/Recepcion/Recepcion_Encabezado/clsLnTrans_re_enc_Partial.vb').read_text('utf-8-sig')
start = code.index('    Private Shared Sub ActualizarLotesCajaMaster(')
end = code.index('\n    End Sub', start) + len('\n    End Sub')
helper = code[start:end]
template = (root / 'tools/diagnostics/test-recepcion-28335-template.vb').read_text('utf-8')
(out / 'TestCajaMaster.vb').write_text(template.replace("'PRODUCTION_HELPER", helper), 'utf-8-sig')
(out / 'TestCajaMaster.vbproj').write_text('''<Project ToolsVersion="Current" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
<PropertyGroup><OutputType>Exe</OutputType><AssemblyName>TestCajaMaster</AssemblyName><TargetFrameworkVersion>v4.8</TargetFrameworkVersion><OutputPath>bin\\</OutputPath><RootNamespace></RootNamespace><OptionStrict>Off</OptionStrict><OptionInfer>On</OptionInfer></PropertyGroup>
<ItemGroup><Reference Include="System"/><Reference Include="System.Core"/><Reference Include="System.Data"/><Reference Include="Entity"><HintPath>'''+str(bof / 'TOMIMSV4/Entity/bin/Debug/Entity.dll')+'''</HintPath></Reference><Compile Include="TestCajaMaster.vb"/></ItemGroup><Import Project="$(MSBuildToolsPath)\\Microsoft.VisualBasic.targets"/></Project>''', 'utf-8')
msbuild = 'C:/Program Files/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe'
subprocess.run([msbuild, str(out / 'TestCajaMaster.vbproj'), '/v:minimal', '/nologo'], check=True)
subprocess.run([str(out / 'bin/TestCajaMaster.exe')], check=True)
