using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace GestaoFacil.App.Exrensions
{
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page,  int tipoPessoa,string documento)
        {
            return tipoPessoa == 2 ? Convert.ToUInt32(documento).ToString(@"000\.000\.000\-00") :
                Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
