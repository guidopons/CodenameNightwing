﻿using CodenameNightwing.Autorization.NPS;
using CodenameNightwing.Autorization.POS;
using CodenameNightwing.Autorization.VTOL;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodenameNightwing.Autorization
{
    public static class AutorizatorFactory
    {
        public static Autorizator getAutorizator( Transaccion tran )
        {

     
            switch (Configuration.getInstance().tipoAuth)
            {
                case TipoAutorizador.POS_INGENICO:
                    return new POSIntegrator();
                case TipoAutorizador.VTOL:
                    return VTOLIntegrator.Instance;
                case TipoAutorizador.VTOL_CALLCENTER:

                    // si está online VTOL y es ARS y no está forzado NPS = VTOL
                    // sino
                    // NPS

                    
                    if (tran != null && tran.currency != null && tran.currency.Equals("ARS") &&
                        !Configuration.getInstance().isNPSForzed  )
                    {
                        Autorizator vtolCallAuth = VTOLIntegratorCallCenter.Instance;
                        Configuration.getInstance().tipoAuth = TipoAutorizador.VTOL_CALLCENTER;
                        return vtolCallAuth;
                    }
                    else
                    {
                        Configuration.getInstance().tipoAuth = TipoAutorizador.NPS;
                        return NPSIntegrator.Instance;
                    }
                case TipoAutorizador.NPS:
                    return NPSIntegrator.Instance;
                default:
                    break;
            }
            return null;
        }


        public static Autorizator getAutorizator()
        {
            return AutorizatorFactory.getAutorizator(null);
        }


    }
}
