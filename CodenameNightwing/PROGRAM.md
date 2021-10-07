### Program.cs 
### argumentos validos linea de comandos:
###  

setProxyException: 

Agrega al proxy las siguentes excepciones (graba en Registry)
"*10.100.6.128*";"*10.100.36.30*";"*aerolineasatiempo*";"*aeroweb*";"<local>";  

###             
configuration: 

Setea Caja,Ciudad,idPrinter,Sucursal,Puesto en aerolinas.properties

###  
tarjetas


###
cashier
payment
void
voidAlone
refund
refundAlone
exchange
exchangeNoi
###
voidTkt
emdIntereses
###
lote
others
###                
printItinerary
printVoucher 
rePrintCupons
###
changePrinter
changePrinterInit
###
getCuil
###
resetPinpad                
killServer
ivr
###                 
 
"VTL"  =  TipoAutorizador.VTOL --> VTOL Pinpad
"VCC"  =  TipoAutorizador.VTOL_CALLCENTER --> IVRPagos
"NPS"  =  TipoAutorizador.NPS --> Gateway NPS

"ING"  =  TipoAutorizador.POS_INGENICO
"HAS"  =  TipoAutorizador.HASAR          