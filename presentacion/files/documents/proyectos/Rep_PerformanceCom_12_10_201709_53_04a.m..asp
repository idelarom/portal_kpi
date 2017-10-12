<%@ Language=VBScript %>
<%Set conn = server.CreateObject("ADODB.Connection")
  conn.open Application("DefaultConeccion") 
  Set rs = server.CreateObject("ADODB.Recordset")
  Set rsVen = server.CreateObject("ADODB.Recordset")
  Set rsFecha = server.CreateObject("ADODB.Recordset")
  Set rsCuota = server.CreateObject("ADODB.Recordset")

  if Application("ConeccionERP")=1 then
   'Set connSGA =Server.CreateObject("adodb.connection") 
   'connSGA.ConnectionTimeout=50000
   'connSGA.CommandTimeout=50000
   'connSGA.open Application("DefaultConeccionSGA")
   'Set rsSGA = server.CreateObject("ADODB.Recordset")
   
   Set connSGA =Server.CreateObject("adodb.connection") 
   connSGA.ConnectionTimeout=5000000
   connSGA.CommandTimeout=5000000
   connSGA.open Application("DefaultConeccionNAVINFO")
   Set rsSGA = server.CreateObject("ADODB.Recordset")

  End if
      
  SqlVen="select nombre from usuarios where cveusuario='"& request("vendedor")&"'"
  rsVen.Open SqlVen,conn

  Sql = "Select FechaActual=getdate()"
  rsFecha.Open Sql,conn
%>

<%  dim mesSys(12)
MesSys(1)="Enero"
MesSys(2)="Feb"
MesSys(3)="Marzo"
MesSys(4)="Abril"
MesSys(5)="Mayo"
MesSys(6)="Junio"
MesSys(7)="Julio"
MesSys(8)="Agosto"
MesSys(9)="Sept"
MesSys(10)="Oct"
MesSys(11)="Nov"
MesSys(12)="Dic"
%>
<% if request("salida")="1" then %>
<%Response.ContentType="application/vnd.ms-excel"%>
<% end if %>

<!-- #INCLUDE file="adovbs.inc" -->

<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
</HEAD>
<Body bgcolor="#FFFFFF" text="#000000" top = "0">

<%if request("salida")="0" then%>
 <link rel="stylesheet" href="Includes/style.css" type="text/css">
 <p align="center"><STRONG><font size=3>Evaluación de Performance Comercial</font></STRONG></p>
 <p align="center"><STRONG>[<%=ucase(request("Vendedor"))%>]&nbsp;&nbsp;<%=rsVen(0)%></STRONG></p>
 <p>* Importes en Miles Dls.</p>
<%end if%>


<%AnoIni=year(rsFecha("FechaActual"))-1%>
<%AnoFin=year(rsFecha("FechaActual"))%>
<%MesActual=month(rsFecha("FechaActual"))%>

<%
Sql = "exec PerformanceComercial_NuevasOP "
Sql = Sql & "'" & request("vendedor") & "'"

rs.Open Sql,conn
%>
<% Q1AnoAnt_NumOP=0
   Q1AnoAnt_MontoOri=0
   Q1AnoAnt_Margen=0
   Q2AnoAnt_NumOP=0
   Q2AnoAnt_MontoOri=0
   Q2AnoAnt_Margen=0
   Q3AnoAnt_NumOP=0
   Q3AnoAnt_MontoOri=0
   Q3AnoAnt_Margen=0
   Q4AnoAnt_NumOP=0
   Q4AnoAnt_MontoOri=0
   Q4AnoAnt_Margen=0
   Q1AnoAct_NumOP=0
   Q1AnoAct_MontoOri=0
   Q1AnoAct_Margen=0
   Q2AnoAct_NumOP=0
   Q2AnoAct_MontoOri=0
   Q2AnoAct_Margen=0
   Q3AnoAct_NumOP=0
   Q3AnoAct_MontoOri=0
   Q3AnoAct_Margen=0
   Q4AnoAct_NumOP=0
   Q4AnoAct_MontoOri=0
   Q4AnoAct_Margen=0
   TotAnoAnt_NumOP=0
   TotAnoAnt_MontoOri=0
   TotAnoAnt_Margen=0
   TotAnoAct_NumOP=0
   TotAnoAct_MontoOri=0
   TotAnoAct_Margen=0
%>
<%Do while not rs.EOF   
   if rs("Ano")=AnoIni then  
    if rs("Tipo")="Q1" then
      Q1AnoAnt_NumOP=rs("NumOP")
      Q1AnoAnt_MontoOri=rs("MontoOri")
      Q1AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q1AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+Q1AnoAnt_MontoOri
      TotAnoAnt_Margen=TotAnoAnt_Margen+Q1AnoAnt_Margen
      elseif rs("Tipo")="Q2" then
      Q2AnoAnt_NumOP=rs("NumOP")
      Q2AnoAnt_MontoOri=rs("MontoOri")
      Q2AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q2AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+Q2AnoAnt_MontoOri
      TotAnoAnt_Margen=TotAnoAnt_Margen+Q2AnoAnt_Margen
      elseif rs("Tipo")="Q3" then
      Q3AnoAnt_NumOP=rs("NumOP")
      Q3AnoAnt_MontoOri=rs("MontoOri")
      Q3AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q3AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+Q3AnoAnt_MontoOri
      TotAnoAnt_Margen=TotAnoAnt_Margen+Q3AnoAnt_Margen
      elseif rs("Tipo")="Q4" then
      Q4AnoAnt_NumOP=rs("NumOP")
      Q4AnoAnt_MontoOri=rs("MontoOri")
      Q4AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q4AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+Q4AnoAnt_MontoOri
      TotAnoAnt_Margen=TotAnoAnt_Margen+Q4AnoAnt_Margen
    end if    
   else
    if rs("Ano")=AnoFin then
     if rs("Tipo")="Q1" then
      Q1AnoAct_NumOP=rs("NumOP")
      Q1AnoAct_MontoOri=rs("MontoOri")
      Q1AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q1AnoAct_NumOP
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+Q1AnoAct_MontoOri
      TotAnoAct_Margen=TotAnoAct_Margen+Q1AnoAct_Margen
      elseif rs("Tipo")="Q2" then
      Q2AnoAct_NumOP=rs("NumOP")
      Q2AnoAct_MontoOri=rs("MontoOri")
      Q2AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q2AnoAct_NumOP
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+Q2AnoAct_MontoOri
      TotAnoAct_Margen=TotAnoAct_Margen+Q2AnoAct_Margen
      elseif rs("Tipo")="Q3" then
      Q3AnoAct_NumOP=rs("NumOP")
      Q3AnoAct_MontoOri=rs("MontoOri")
      Q3AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q3AnoAct_NumOP
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+Q3AnoAct_MontoOri
      TotAnoAct_Margen=TotAnoAct_Margen+Q3AnoAct_Margen
      elseif rs("Tipo")="Q4" then
      Q4AnoAct_NumOP=rs("NumOP")
      Q4AnoAct_MontoOri=rs("MontoOri")
      Q4AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q4AnoAct_NumOP             
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+Q4AnoAct_MontoOri
      TotAnoAct_Margen=TotAnoAct_Margen+Q4AnoAct_Margen
     end if
    end if           
   end if
                
   rs.MoveNext
  loop    
  
  %>

<table width=100% border="1" cellspacing="0" cellpadding="0" bordercolor=Black>
  <tr>
   <td width=25% rowspan=2 bgcolor=silver><b>Nuevas Oportunidades</b></td>   
   <td colspan=3 align=center><b><%=year(rsFecha("FechaActual"))-1%></b></td>
   <td rowspan=7 width=5px bgcolor=silver><br></td>
   <td colspan=3 align=center><b><%=year(rsFecha("FechaActual"))%></b></td>
  </tr>
  <tr>   
   <td align=center bgcolor=Silver>Op's</td>
   <td align=center bgcolor=Silver>Monto Original</td>
   <td align=center bgcolor=Silver>Margen</td>   
   
   <td align=center bgcolor=Silver>Op's</td>
   <td align=center bgcolor=Silver>Monto Original</td>
   <td align=center bgcolor=Silver>Margen</td>   
  </tr>
  <tr>   
   <td>Q1</td>
   
   <td align=center><%=Q1AnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(Q1AnoAnt_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q1AnoAnt_Margen/1000,2)%> K</td>
   
   <td align=center><%=Q1AnoAct_NumOP%></td>
   <td align=right><%=formatcurrency(Q1AnoAct_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q1AnoAct_Margen/1000,2)%> K</td>
   
  </tr>
  <tr>    
   <td>Q2</td>
   
   <td align=center><%=Q2AnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(Q2AnoAnt_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q2AnoAnt_Margen/1000,2)%> K</td>
   
   <td align=center><%=Q2AnoAct_NumOP%></td>
   <td align=right><%=formatcurrency(Q2AnoAct_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q2AnoAct_Margen/1000,2)%> K</td>
  </tr>
  <tr>      
   <td>Q3</td>
   
   <td align=center><%=Q3AnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(Q3AnoAnt_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q3AnoAnt_Margen/1000,2)%> K</td>    
   
   <td align=center><%=Q3AnoAct_NumOP%></td>
   <td align=right><%=formatcurrency(Q3AnoAct_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q3AnoAct_Margen/1000,2)%> K</td>
  </tr>
  <tr>   
   <td>Q4</td>
   
   <td align=center><%=Q4AnoAnt_NumOP%></td>   
   <td align=right><%=formatcurrency(Q4AnoAnt_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q4AnoAnt_Margen/1000,2)%> K</td>
   
   <td align=center><%=Q4AnoAct_NumOP%></td>   
   <td align=right><%=formatcurrency(Q4AnoAct_MontoOri/1000,2)%> K</td>
   <td align=right><%=formatcurrency(Q4AnoAct_Margen/1000,2)%> K</td>
  </tr>
  <tr>   
   <td><b>Total</b></td>
   
   <td align=center><b><%=TotAnoAnt_NumOP%></b></td>   
   <td align=right><b><%=formatcurrency(TotAnoAnt_MontoOri/1000,2)%> K</td>
   <td align=right><b><%=formatcurrency(TotAnoAnt_Margen/1000,2)%> K</td>
   
   <td align=center><b><%=TotAnoAct_NumOP%></b></td>   
   <td align=right><b><%=formatcurrency(TotAnoAct_MontoOri/1000,2)%> K</b></td>
   <td align=right><b><%=formatcurrency(TotAnoAct_Margen/1000,2)%> K</b></td>
  </tr>
  </table>


<br>

<% rs.Close%>

<%
 Sql = "exec PerformanceComercial_InventarioOP "
 Sql = Sql & "'" & request("vendedor") & "'"

 rs.Open Sql,conn
%>

<% MenorAnoAnt_NumOP=0
   MenorAnoAnt_MontoOri=0
   MenorAnoAnt_Margen=0
   Q1AnoAnt_NumOP=0
   Q1AnoAnt_MontoOri=0
   Q1AnoAnt_Margen=0
   Q2AnoAnt_NumOP=0
   Q2AnoAnt_MontoOri=0
   Q2AnoAnt_Margen=0
   Q3AnoAnt_NumOP=0
   Q3AnoAnt_MontoOri=0
   Q3AnoAnt_Margen=0
   Q4AnoAnt_NumOP=0
   Q4AnoAnt_MontoOri=0
   Q4AnoAnt_Margen=0
   Q1AnoAct_NumOP=0
   Q1AnoAct_MontoOri=0
   Q1AnoAct_Margen=0
   Q2AnoAct_NumOP=0
   Q2AnoAct_MontoOri=0
   Q2AnoAct_Margen=0
   Q3AnoAct_NumOP=0
   Q3AnoAct_MontoOri=0
   Q3AnoAct_Margen=0
   Q4AnoAct_NumOP=0
   Q4AnoAct_MontoOri=0
   Q4AnoAct_Margen=0   
   TotAnoAnt_NumOP=0
   TotAnoAnt_MontoOri=0
   TotAnoAnt_Margen=0
   TotAnoAct_NumOP=0
   TotAnoAct_MontoOri=0
   TotAnoAct_Margen=0
   TotInven_NumOP=0
   TotInven_MontoOri=0
   TotInven_Margen=0
%>
  
<%Do while not rs.EOF   
   if rs("Ano")=AnoIni then
    if rs("Tipo")="<" then
      MenorAnoAnt_NumOp=rs("NumOP")
      MenorAnoAnt_MontoOri=round(rs("MontoOri")/1000,0)
      MenorAnoAnt_Margen=round(rs("Margen")/1000,0)
      elseif rs("Tipo")="Q1" then
      Q1AnoAnt_NumOP=rs("NumOP")
      Q1AnoAnt_MontoOri=rs("MontoOri")
      Q1AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q1AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+round(Q1AnoAnt_MontoOri/1000,0)
      TotAnoAnt_Margen=TotAnoAnt_Margen+round(Q1AnoAnt_Margen/1000,0)
      elseif rs("Tipo")="Q2" then
      Q2AnoAnt_NumOP=rs("NumOP")
      Q2AnoAnt_MontoOri=rs("MontoOri")
      Q2AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q2AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+round(Q2AnoAnt_MontoOri/1000,0)
      TotAnoAnt_Margen=TotAnoAnt_Margen+round(Q2AnoAnt_Margen/1000,0)
      elseif rs("Tipo")="Q3" then
      Q3AnoAnt_NumOP=rs("NumOP")
      Q3AnoAnt_MontoOri=rs("MontoOri")
      Q3AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q3AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+round(Q3AnoAnt_MontoOri/1000,0)
      TotAnoAnt_Margen=TotAnoAnt_Margen+round(Q3AnoAnt_Margen/1000,0)
      elseif rs("Tipo")="Q4" then
      Q4AnoAnt_NumOP=rs("NumOP")
      Q4AnoAnt_MontoOri=rs("MontoOri")
      Q4AnoAnt_Margen=rs("Margen")
      TotAnoAnt_NumOP=TotAnoAnt_NumOP+Q4AnoAnt_NumOP
      TotAnoAnt_MontoOri=TotAnoAnt_MontoOri+round(Q4AnoAnt_MontoOri/1000,0)
      TotAnoAnt_Margen=TotAnoAnt_Margen+round(Q4AnoAnt_Margen/1000,0)
    end if    
   else
    if rs("Ano")=AnoFin then
     if rs("Tipo")="Q1" then
      Q1AnoAct_NumOP=rs("NumOP")
      Q1AnoAct_MontoOri=rs("MontoOri")
      Q1AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q1AnoAct_NumOP
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+round(Q1AnoAct_MontoOri/1000,0)
      TotAnoAct_Margen=TotAnoAct_Margen+round(Q1AnoAct_Margen/1000,0)
      elseif rs("Tipo")="Q2" then
      Q2AnoAct_NumOP=rs("NumOP")
      Q2AnoAct_MontoOri=rs("MontoOri")
      Q2AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q2AnoAct_NumOP
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+round(Q2AnoAct_MontoOri/1000,0)
      TotAnoAct_Margen=TotAnoAct_Margen+round(Q2AnoAct_Margen/1000,0)
      elseif rs("Tipo")="Q3" then
      Q3AnoAct_NumOP=rs("NumOP")
      Q3AnoAct_MontoOri=rs("MontoOri")
      Q3AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q3AnoAct_NumOP
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+round(Q3AnoAct_MontoOri/1000,0)
      TotAnoAct_Margen=TotAnoAct_Margen+round(Q3AnoAct_Margen/1000,0)
      elseif rs("Tipo")="Q4" then
      Q4AnoAct_NumOP=rs("NumOP")
      Q4AnoAct_MontoOri=rs("MontoOri")
      Q4AnoAct_Margen=rs("Margen")
      TotAnoAct_NumOP=TotAnoAct_NumOP+Q4AnoAct_NumOP             
      TotAnoAct_MontoOri=TotAnoAct_MontoOri+round(Q4AnoAct_MontoOri/1000,0)
      TotAnoAct_Margen=TotAnoAct_Margen+round(Q4AnoAct_Margen/1000,0)
     end if
    end if           
   end if
                
   rs.MoveNext
  loop
  
  TotInvent_NumOP=MenorAnoAnt_NumOp+TotAnoAnt_NumOP+TotAnoAct_NumOP
  TotInvent_MontoOri=MenorAnoAnt_MontoOri+TotAnoAnt_MontoOri+TotAnoAct_MontoOri
  TotInvent_Margen=MenorAnoAnt_Margen+TotAnoAnt_Margen+TotAnoAct_Margen
   %>
  
    
  <table width=100% border="1" cellspacing="0" cellpadding="0" bordercolor=Black>
  <tr>
   <td width=25% rowspan=2 bgcolor=silver><b>Inventario de Oportunidades</b></td>
   <td colspan=3 align=center><b>T o t a l e s</b></td>   
   <td rowspan=6 width=5px bgcolor=silver><br></td>
   <td colspan=4 align=center><b><%=year(rsFecha("FechaActual"))-1%></b></td>
   <td rowspan=6 width=5px bgcolor=silver><br></td>
   <td colspan=3 align=center><b><%=year(rsFecha("FechaActual"))%></b></td>
  </tr>
  <tr>
   <td align=center bgcolor=Silver>Op's</td>
   <td align=center bgcolor=Silver>Monto Original</td>
   <td align=center bgcolor=Silver>Margen</td>   

   <td bgcolor=silver><br></td>
   <td align=center bgcolor=Silver>Op's</td>
   <td align=center bgcolor=Silver>Monto Original</td>
   <td align=center bgcolor=Silver>Margen</td>   
   
   <td align=center bgcolor=Silver>Op's</td>
   <td align=center bgcolor=Silver>Monto Original</td>
   <td align=center bgcolor=Silver>Margen</td>   
  </tr>
  <tr>
   <td>Anteriores a <%=year(rsFecha("FechaActual"))-1%></td>
   <td align=center><%=MenorAnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(MenorAnoAnt_MontoOri,0)%> K</td>
   <td align=right><%=formatcurrency(MenorAnoAnt_Margen,0)%> K</td>
   
   <td align=center>Q1</td>
   
   <td align=center><%=Q1AnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(round(Q1AnoAnt_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q1AnoAnt_Margen/1000,0),0)%> K</td>
   
   <td align=center><%=Q1AnoAct_NumOP%></td>
   <td align=right><%=formatcurrency(round(Q1AnoAct_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q1AnoAct_Margen/1000,0),0)%> K</td>
   
  </tr>
  <tr> 
   <td><%=year(rsFecha("FechaActual"))-1%></td>
   <td align=center><%=TotAnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(TotAnoAnt_MontoOri,0)%> K</td>
   <td align=right><%=formatcurrency(TotAnoAnt_Margen,0)%> K</td>
   
   <td align=center>Q2</td>
   
   <td align=center><%=Q2AnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(round(Q2AnoAnt_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q2AnoAnt_Margen/1000,0),0)%> K</td>
   
   <td align=center><%=Q2AnoAct_NumOP%></td>
   <td align=right><%=formatcurrency(round(Q2AnoAct_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q2AnoAct_Margen/1000,0),0)%> K</td>
  </tr>
  <tr> 
   <td><%=year(rsFecha("FechaActual"))%></td>   
   <td align=center><%=TotAnoAct_NumOP%></td>
   <td align=right><%=formatcurrency(TotAnoAct_MontoOri,0)%> K</td>
   <td align=right><%=formatcurrency(TotAnoAct_Margen,0)%> K</td>
   
   <td align=center>Q3</td>
   
   <td align=center><%=Q3AnoAnt_NumOP%></td>
   <td align=right><%=formatcurrency(round(Q3AnoAnt_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q3AnoAnt_Margen/1000,0),0)%> K</td>    
   
   <td align=center><%=Q3AnoAct_NumOP%></td>
   <td align=right><%=formatcurrency(round(Q3AnoAct_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q3AnoAct_Margen/1000,0),0)%> K</td>
  </tr>
  <tr>
   <td><b>Total</b></td>
   <td align=center><b><%=TotInvent_NumOP%></b></td>
   <td align=right><b><%=formatcurrency(TotInvent_MontoOri,0)%> K</b></td>
   <td align=right><b><%=formatcurrency(TotInvent_Margen,0)%> K</b></td>
   
   <td align=center>Q4</td>
   
   <td align=center><%=Q4AnoAnt_NumOP%></td>   
   <td align=right><%=formatcurrency(round(Q4AnoAnt_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q4AnoAnt_Margen/1000,0),0)%> K</td>
   
   <td align=center><%=Q4AnoAct_NumOP%></td>   
   <td align=right><%=formatcurrency(round(Q4AnoAct_MontoOri/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(Q4AnoAct_Margen/1000,0),0)%> K</td>
  </tr>
  </table>

<br>

<% rs.Close%>

<%
 Sql = "exec PerformanceComercial_CierreOP "
 Sql = Sql & "'" & request("vendedor") & "'"

 rs.Open Sql,conn
%>

  
<% 
   AnoAnt_G_NumOP=0   
   AnoAnt_G_MontoFinal=0
   AnoAnt_G_MargenFinal=0   
   AnoAnt_G_DiasCierre=0

   AnoAnt_P_NumOP=0   
   AnoAnt_P_MontoFinal=0
   AnoAnt_P_MargenFinal=0   
   AnoAnt_P_DiasCierre=0
   
   AnoAnt_C_NumOP=0   
   AnoAnt_C_MontoFinal=0
   AnoAnt_C_MargenFinal=0   
   AnoAnt_C_DiasCierre=0
   
   AnoAnt_A_NumOP=0   
   AnoAnt_A_MontoFinal=0
   AnoAnt_A_MargenFinal=0   
   AnoAnt_A_DiasCierre=0
   
   AnoAct_G_NumOP=0   
   AnoAct_G_MontoFinal=0
   AnoAct_G_MargenFinal=0   
   Anoact_G_DiasCierre=0

   AnoAct_P_NumOP=0   
   AnoAct_P_MontoFinal=0
   AnoAct_P_MargenFinal=0   
   AnoAct_P_DiasCierre=0
   
   AnoAct_C_NumOP=0   
   AnoAct_C_MontoFinal=0
   AnoAct_C_MargenFinal=0   
   AnoAct_C_DiasCierre=0
   
   AnoAct_A_NumOP=0   
   AnoAct_A_MontoFinal=0
   AnoAct_A_MargenFinal=0   
   AnoAct_A_DiasCierre=0
   
   TotAnoAnt_NumOP=0   
   TotAnoAnt_MontoFinal=0
   TotAnoAnt_MargenFinal=0
   
   TotAnoAct_NumOP=0   
   TotAnoAct_MontoFinal=0
   TotAnoAct_MargenFinal=0        
%>
  
<%Do while not rs.EOF   
   if rs("Ano")=AnoIni then  
    if rs("IdEstatus")="G" then     
     AnoAnt_G_NumOP=rs("NumOP")     
     AnoAnt_G_MontoFinal=rs("MontoFinal")
     AnoAnt_G_MargenFinal=rs("MargenFinal")    
     AnoAnt_G_DiasCierre=rs("DiasCierre")    
     TotAnoAnt_NumOP=TotAnoAnt_NumOP+AnoAnt_G_NumOP
     TotAnoAnt_MontoFinal=TotAnoAnt_MontoFinal+AnoAnt_G_MontoFinal
     TotAnoAnt_MargenFinal=TotAnoAnt_MargenFinal+AnoAnt_G_MargenFinal
    elseif rs("IdEstatus")="P" then     
     AnoAnt_P_NumOP=rs("NumOP")     
     AnoAnt_P_MontoFinal=rs("MontoFinal")
     AnoAnt_P_MargenFinal=rs("MargenFinal")    
     AnoAnt_P_DiasCierre=rs("DiasCierre")
     TotAnoAnt_NumOP=TotAnoAnt_NumOP+AnoAnt_P_NumOP    
     TotAnoAnt_MontoFinal=TotAnoAnt_MontoFinal+AnoAnt_P_MontoFinal
     TotAnoAnt_MargenFinal=TotAnoAnt_MargenFinal+AnoAnt_P_MargenFinal
    elseif rs("IdEstatus")="C" then     
     AnoAnt_C_NumOP=rs("NumOP")     
     AnoAnt_C_MontoFinal=rs("MontoFinal")
     AnoAnt_C_MargenFinal=rs("MargenFinal")    
     AnoAnt_C_DiasCierre=rs("DiasCierre")    
     TotAnoAnt_NumOP=TotAnoAnt_NumOP+AnoAnt_C_NumOP
     TotAnoAnt_MontoFinal=TotAnoAnt_MontoFinal+AnoAnt_C_MontoFinal
     TotAnoAnt_MargenFinal=TotAnoAnt_MargenFinal+AnoAnt_C_MargenFinal
    elseif rs("IdEstatus")="A" then     
     AnoAnt_A_NumOP=rs("NumOP")     
     AnoAnt_A_MontoFinal=rs("MontoFinal")
     AnoAnt_A_MargenFinal=rs("MargenFinal")    
     AnoAnt_A_DiasCierre=rs("DiasCierre")
     TotAnoAnt_NumOP=TotAnoAnt_NumOP+AnoAnt_A_NumOP
     TotAnoAnt_MontoFinal=TotAnoAnt_MontoFinal+AnoAnt_A_MontoFinal
     TotAnoAnt_MargenFinal=TotAnoAnt_MargenFinal+AnoAnt_A_MargenFinal
    end if 
   else
    if rs("Ano")=AnoFin then
     if rs("IdEstatus")="G" then     
      AnoAct_G_NumOP=rs("NumOP")     
      AnoAct_G_MontoFinal=rs("MontoFinal")
      AnoAct_G_MargenFinal=rs("MargenFinal")    
      AnoAct_G_DiasCierre=rs("DiasCierre")    
      TotAnoAct_NumOP=TotAnoAct_NumOP+AnoAct_G_NumOP
      TotAnoAct_MontoFinal=TotAnoAct_MontoFinal+AnoAct_G_MontoFinal
      TotAnoAct_MargenFinal=TotAnoAct_MargenFinal+AnoAct_G_MargenFinal
     elseif rs("IdEstatus")="P" then     
      AnoAct_P_NumOP=rs("NumOP")     
      AnoAct_P_MontoFinal=rs("MontoFinal")
      AnoAct_P_MargenFinal=rs("MargenFinal")    
      AnoAct_P_DiasCierre=rs("DiasCierre")
      TotAnoAct_NumOP=TotAnoAct_NumOP+AnoAct_P_NumOP
      TotAnoAct_MontoFinal=TotAnoAct_MontoFinal+AnoAct_P_MontoFinal
      TotAnoAct_MargenFinal=TotAnoAct_MargenFinal+AnoAct_P_MargenFinal    
     elseif rs("IdEstatus")="C" then     
      AnoAct_C_NumOP=rs("NumOP")     
      AnoAct_C_MontoFinal=rs("MontoFinal")
      AnoAct_C_MargenFinal=rs("MargenFinal")    
      AnoAct_C_DiasCierre=rs("DiasCierre")
      TotAnoAct_NumOP=TotAnoAct_NumOP+AnoAct_C_NumOP
      TotAnoAct_MontoFinal=TotAnoAct_MontoFinal+AnoAct_C_MontoFinal
      TotAnoAct_MargenFinal=TotAnoAct_MargenFinal+AnoAct_C_MargenFinal    
     elseif rs("IdEstatus")="A" then     
      AnoAct_A_NumOP=rs("NumOP")     
      AnoAct_A_MontoFinal=rs("MontoFinal")
      AnoAct_A_MargenFinal=rs("MargenFinal")    
      AnoAct_A_DiasCierre=rs("DiasCierre")    
      TotAnoAct_NumOP=TotAnoAct_NumOP+AnoAct_A_NumOP
      TotAnoAct_MontoFinal=TotAnoAct_MontoFinal+AnoAct_A_MontoFinal
      TotAnoAct_MargenFinal=TotAnoAct_MargenFinal+AnoAct_A_MargenFinal
     end if
    end if
   end if                
   rs.MoveNext
  loop
  %>

<table width=100% border="1" cellspacing="0" cellpadding="0" bordercolor=Black>
  <tr>
   <td width=25% rowspan=2 bgcolor=silver><b>% de Bateo</b></td>   
   <td colspan=4 align=center><b><%=year(rsFecha("FechaActual"))-1%></b></td>
   <td rowspan=7 width=5px bgcolor=silver><br></td>
   <td colspan=4 align=center><b><%=year(rsFecha("FechaActual"))%></b></td>
  </tr>
  <tr>   
   <td align=center bgcolor=Silver>Op's</td>
   <td align=center bgcolor=Silver>Monto Final</td>
   <td align=center bgcolor=Silver>Margen</td>   
   <td align=center bgcolor=Silver>Días/Cierre</td>   
   
   <td align=center bgcolor=Silver>Op's</td>
   <td align=center bgcolor=Silver>Monto Final</td>
   <td align=center bgcolor=Silver>Margen</td>   
   <td align=center bgcolor=Silver>Días/Cierre</td>   
  </tr>
  <tr>   
   <td>Ganadas</td>   
   <td align=center><%=AnoAnt_G_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAnt_G_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAnt_G_MargenFinal/1000,0),0)%> K</td>
   <%if AnoAnt_G_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><%=round(round(AnoAnt_G_DiasCierre/AnoAnt_G_NumOP,0),0)%></td>
   <%end if%> 
   
   <td align=center><%=AnoAct_G_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAct_G_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAct_G_MargenFinal/1000,0),0)%> K</td>
   <%if AnoAct_G_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><%=round(AnoAct_G_DiasCierre/AnoAct_G_NumOP,0)%></td>
   <%end if%>
   
  </tr>
  <tr>    
   <td>Perdidas</td>
   <td align=center><%=AnoAnt_P_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAnt_P_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAnt_P_MargenFinal/1000,0),0)%> K</td>
   <td><br></td>
   
   <td align=center><%=AnoAct_P_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAct_P_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAct_P_MargenFinal/1000,0),0)%> K</td>
   <td><br></td>
  </tr>
  <tr>      
   <td>Canceladas</td>   
   <td align=center><%=AnoAnt_C_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAnt_C_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAnt_C_MargenFinal/1000,0),0)%> K</td>
   <td><br></td>
   
   <td align=center><%=AnoAct_C_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAct_C_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAct_C_MargenFinal/1000,0),0)%> K</td>
   <td><br></td>
  </tr>
  <tr>   
   <td>Abandonadas</td>    
   <td align=center><%=AnoAnt_A_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAnt_A_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAnt_A_MargenFinal/1000,0),0)%> K</td>
   <td><br></td>
   
   <td align=center><%=AnoAct_A_NumOP%></td>
   <td align=right><%=formatcurrency(round(AnoAct_A_MontoFinal/1000,0),0)%> K</td>
   <td align=right><%=formatcurrency(round(AnoAct_A_MargenFinal/1000,0),0)%> K</td>
   <td><br></td>
  </tr>
  <tr>   
   <td><b>Total</b></td>         
   <td align=center><b><%=TotAnoAnt_NumOP%></b></td>   
   <td align=right><b><%=formatcurrency(round(TotAnoAnt_MontoFinal/1000,0),0)%> K</td>
   <td align=right><b><%=formatcurrency(round(TotAnoAnt_MargenFinal/1000,0),0)%> K</td>
   <td><br></td>
   
   <td align=center><b><%=TotAnoAct_NumOP%></b></td>   
   <td align=right><b><%=formatcurrency(round(TotAnoAct_MontoFinal/1000,0),0)%> K</b></td>
   <td align=right><b><%=formatcurrency(round(TotAnoAct_MargenFinal/1000,0),0)%> K</b></td>
   <td><br></td>
  </tr>
  <tr>
   <td height=20 colspan=10><br></td>   
  </tr>
  <tr bgcolor=Green>
   <td><font color=White>Ganadas</td>
   <%if TotAnoAnt_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAnt_G_NumOP/TotAnoAnt_NumOP)*100,1),1)%>%</td>
   <%end if%> 
   <%if TotAnoAnt_MontoFinal=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_G_MontoFinal/1000,0)/round(TotAnoAnt_MontoFinal/1000,0))*100,1),1)%>%</td>
   <%end if%> 
   <%if TotAnoAnt_MargenFinal=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_G_MargenFinal/1000,0)/round(TotAnoAnt_MargenFinal/1000,0))*100,1),1)%>%</td>
   <%end if%> 
   <td><br></td>
   
   <td><br></td>
   <%if TotAnoAct_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAct_G_NumOP/TotAnoAct_NumOP)*100,1),1)%>%</td>
   <%end if%>
   
   
   <%if TotAnoAct_MontoFinal or round(TotAnoAct_MontoFinal/1000,0)=0 then%>
    <td><br></td>
   <%else%>  
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAct_G_MontoFinal/1000,0)/round(TotAnoAct_MontoFinal/1000,0))*100,1),1)%></td>
   <%end if%>
   
   <%if TotAnoAct_MargenFinal=0 or round(TotAnoAct_MargenFinal/1000,0)=0 then%>
    <td><br></td>
   <%else%>  
    <td align=center><font color=White>
		<%=FormatNumber(round((round(AnoAct_G_MargenFinal/1000,0)/round(TotAnoAct_MargenFinal/1000,0))*100,1),1)%>%				
	</td>   
   <%end if%> 
   <td><br></td>
  </tr>
  <tr bgcolor=Red>
   <td><font color=White>Perdidas</td>
   <%if TotAnoAnt_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAnt_P_NumOP/TotAnoAnt_NumOP)*100,1),1)%>%</td>
   <%end if%>
   
   <%if TotAnoAnt_MontoFinal=0 then%>
    <td><br></td>
   <%else%>  
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_P_MontoFinal/1000,0)/round(TotAnoAnt_MontoFinal/1000,0))*100,1),1)%>%</td>
   <%end if%>
   
   <%if TotAnoAnt_MargenFinal=0 then%>
    <td><br></td>
   <%else%>  
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_P_MargenFinal/1000,0)/round(TotAnoAnt_MargenFinal/1000,0))*100,1),1)%>%</td>
   <%end if%> 
   <td><br></td>
   
   <td><br></td>
   <%if TotAnoAct_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAct_P_NumOP/TotAnoAct_NumOP)*100,1),1)%>%</td>
   <%end if%>
   
   <%if TotAnoAct_MontoFinal=0 or round(TotAnoAct_MontoFinal/1000,0)=0 then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAct_P_MontoFinal/1000,0)/round(TotAnoAct_MontoFinal/1000,0))*100,1),1)%>%</td>
   <%end if%>
   
   <%if TotAnoAct_MargenFinal=0 or round(TotAnoAct_MargenFinal/1000,0)=0 then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAct_P_MargenFinal/1000,0)/round(TotAnoAct_MargenFinal/1000,0))*100,1),1)%>%</td>
   <%end if%> 
   <td><br></td>
  </tr>
  <tr bgcolor=Black>
   <td><font color=White>Canceladas</td>
   <%if TotAnoAnt_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAnt_C_NumOP/TotAnoAnt_NumOP)*100,1),1)%>%</td>
   <%end if%>
   <%if TotAnoAnt_MontoFinal=0 then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_C_MontoFinal/1000,0)/round(TotAnoAnt_MontoFinal/1000,0))*100,1),1)%>%</td>
   <%end if%>
   <%if TotAnoAnt_MargenFinal=0 then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_C_MargenFinal/1000,0)/round(TotAnoAnt_MargenFinal/1000,0))*100,1),1)%>%</td>
   <%end if%> 
   <td><br></td>
   
   <td><br></td>
   <%if TotAnoAct_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAct_C_NumOP/TotAnoAct_NumOP)*100,1),1)%>%</td>
   <%end if%> 
   
   <%if TotAnoAct_MontoFinal=0 or round(TotAnoAct_MontoFinal/1000,0)=0 then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAct_C_MontoFinal/1000,0)/round(TotAnoAct_MontoFinal/1000,0))*100,1),1)%>%</td>
   <%end if%> 
   
   <%if TotAnoAct_MargenFinal=0 or round(TotAnoAct_MargenFinal/1000,0)=0 then%>
    <td><br></td>
   <%else%>  
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAct_C_MargenFinal/1000,0)/round(TotAnoAct_MargenFinal/1000,0))*100,1),1)%>%</td>   
   <%end if%> 
   <td><br></td>
  </tr>
  <tr bgcolor=Black>
   <td><font color=White>Abandonadas</td>
   <%if TotAnoAnt_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAnt_A_NumOP/TotAnoAnt_NumOP)*100,1),1)%>%</td>
   <%end if%> 
   <%if TotAnoAnt_MontoFinal=0 then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_A_MontoFinal/1000,0)/round(TotAnoAnt_MontoFinal/1000,0))*100,1),1)%>%</td>
   <%end if%>
   <%if TotAnoAnt_MargenFinal=0 then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAnt_A_MargenFinal/1000,0)/round(TotAnoAnt_MargenFinal/1000,0))*100,1),1)%>%</td>   
   <%end if%> 
   <td><br></td>
   
   <td><br></td>
   <%if TotAnoAct_NumOP=0 then%>
    <td><br></td>
   <%else%> 
    <td align=center><font color=White><%=FormatNumber(round((AnoAct_A_NumOP/TotAnoAct_NumOP)*100,1),1)%>%</td>
   <%end if%>
   
   <%if TotAnoAct_MontoFinal=0 or round(TotAnoAct_MontoFinal/1000,0)=0 then%>
    <td><br></td>
   <%else%>    
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAct_A_MontoFinal/1000,0)/round(TotAnoAct_MontoFinal/1000,0))*100,1),1)%>%</td>
   <%end if%>
   
   <%if TotAnoAct_MargenFinal=0 or round(TotAnoAct_MargenFinal/1000,0)=0  then%>
    <td><br></td>
   <%else%>   
    <td align=center><font color=White><%=FormatNumber(round((round(AnoAct_A_MargenFinal/1000,0)/round(TotAnoAct_MargenFinal/1000,0))*100,1),1)%>%</td>      
   <%end if%> 
   <td><br></td>
  </tr>
  </table> 
 
  <br>
  
<% rs.Close %>

<table width="100%" border="0" cellspacing="0" cellpadding="0" bordercolor=Black>
<tr> 
 <%if Application("ConeccionERP")=1 then%>
 <td valign=top width=70%>  
 <%
   SqlSGA = "Exec NAVISION_VtaCtoMar_VenDLLS "
   SqlSGA = SqlSGA & "'" & request("vendedor") & "','" & AnoIni &"'"
   'Response.Write SqlSGA
   rsSGA.Open SqlSGA,connSGA      
   'rsSGA=connSGA.Execute(SqlSGA)
   
   
   Q1AnoAnt_Ventas=0
   Q1AnoAnt_Margen=0
   Q1AnoAnt_Cuota=0
   Q2AnoAnt_Ventas=0
   Q2AnoAnt_Margen=0
   Q2AnoAnt_Cuota=0
   Q3AnoAnt_Ventas=0
   Q3AnoAnt_Margen=0
   Q3AnoAnt_Cuota=0
   Q4AnoAnt_Ventas=0
   Q4AnoAnt_Margen=0
   Q4AnoAnt_Cuota=0
   
   M1AnoAct_Ventas=0
   M1AnoAct_Margen=0
   M1AnoAct_Cuota=0
   M2AnoAct_Ventas=0
   M2AnoAct_Margen=0
   M2AnoAct_Cuota=0
   M3AnoAct_Ventas=0
   M3AnoAct_Margen=0
   M3AnoAct_Cuota=0
   M4AnoAct_Ventas=0
   M4AnoAct_Margen=0
   M4AnoAct_Cuota=0   
   M5AnoAct_Ventas=0
   M5AnoAct_Margen=0
   M5AnoAct_Cuota=0
   M6AnoAct_Ventas=0
   M6AnoAct_Margen=0
   M6AnoAct_Cuota=0
   M7AnoAct_Ventas=0
   M7AnoAct_Margen=0
   M7AnoAct_Cuota=0
   M8AnoAct_Ventas=0
   M8AnoAct_Margen=0
   M8AnoAct_Cuota=0   
   M9AnoAct_Ventas=0
   M9AnoAct_Margen=0
   M9AnoAct_Cuota=0
   M10AnoAct_Ventas=0
   M10AnoAct_Margen=0
   M10AnoAct_Cuota=0
   M11AnoAct_Ventas=0
   M11AnoAct_Margen=0
   M11AnoAct_Cuota=0
   M12AnoAct_Ventas=0
   M12AnoAct_Margen=0
   M12AnoAct_Cuota=0   

   TotAnoAnt_Ventas=0   
   TotAnoAnt_Margen=0
   TotAnoAnt_Cuota=0
   
   TotAnoAct_Ventas=0   
   TotAnoAct_Margen=0
   TotAnoAct_Cuota=0        

  Response.Write rsSGA.recordcount
  Do while not rsSGA.EOF   
   if rsSGA("Año")=AnoIni then  
	'Response.Write rsSGA("Venta")
    if rsSGA("Mes")>=1 and rsSGA("Mes")<=3 then
     Q1AnoAnt_Ventas=Q1AnoAnt_Ventas+formatCurrency(rsSGA("Venta"))
     Q1AnoAnt_Margen=Q1AnoAnt_Margen+formatCurrency(rsSGA("Margen"))
    elseif rsSGA("Mes")>=4 and rsSGA("Mes")<=6 then
     Q2AnoAnt_Ventas=Q2AnoAnt_Ventas+formatCurrency(rsSGA("Venta"))
     Q2AnoAnt_Margen=Q2AnoAnt_Margen+formatCurrency(rsSGA("Margen"))
    elseif rsSGA("Mes")>=7 and rsSGA("Mes")<=9 then
     Q3AnoAnt_Ventas=Q3AnoAnt_Ventas+formatCurrency(rsSGA("Venta"))
     Q3AnoAnt_Margen=Q3AnoAnt_Margen+formatCurrency(rsSGA("Margen"))
    elseif rsSGA("Mes")>=10 and rsSGA("Mes")<=12 then
     Q4AnoAnt_Ventas=Q4AnoAnt_Ventas+formatCurrency(rsSGA("Venta"))
     Q4AnoAnt_Margen=Q4AnoAnt_Margen+formatCurrency(rsSGA("Margen"))
    end if     
   end if            
   rsSGA.MoveNext
  loop
  
  TotAnoAnt_Ventas=Q1AnoAnt_Ventas+Q2AnoAnt_Ventas+Q3AnoAnt_Ventas+Q4AnoAnt_Ventas
  TotAnoAnt_Margen=Q1AnoAnt_Margen+Q2AnoAnt_Margen+Q3AnoAnt_Margen+Q4AnoAnt_Margen
  
  SqlCuota = "Select * from Tblcuotas"
  SqlCuota = SqlCuota & " Where cveusuario='" & request("vendedor") & "' and ano=" & AnoIni
  SqlCUota = SqlCuota & " Order by Ano,cve_mes"
  'Response.Write SqlCuota
  rsCuota.Open SqlCuota,conn

  Do while not rsCuota.EOF   
  if rsCuota("Ano")=AnoIni then
   if rsCuota("Cve_Mes")=1 then   
	M1AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=2 then	
	M2AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=3 then
	M3AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=4 then
    M4AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=5 then
    M5AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=6 then
    M6AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=7 then 
    M7AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=8 then 
    M8AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=9 then 
    M9AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=10 then 
    M10AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=11 then 
    M11AnoAnt_Cuota=rsCuota("Monto_Cuota")
   elseif rsCuota("Cve_Mes")=12 then 
    M12AnoAnt_Cuota=rsCuota("Monto_Cuota")
   end if 
  end if
   
  rsCuota.MoveNext
  Loop  
  
  Q1AnoAnt_Cuota=M1AnoAnt_Cuota+M2AnoAnt_Cuota+M3AnoAnt_Cuota
  Q2AnoAnt_Cuota=M4AnoAnt_Cuota+M5AnoAnt_Cuota+M6AnoAnt_Cuota
  Q3AnoAnt_Cuota=M7AnoAnt_Cuota+M8AnoAnt_Cuota+M9AnoAnt_Cuota
  Q4AnoAnt_Cuota=M10AnoAnt_Cuota+M11AnoAnt_Cuota+M12AnoAnt_Cuota
  
  TotAnoAnt_Cuota=round(Q1AnoAnt_Cuota/1000,0)+round(Q2AnoAnt_Cuota/1000,0)+round(Q3AnoAnt_Cuota/1000,0)+round(Q4AnoAnt_Cuota/1000,0)
  
  rsCuota.Close
  rsSGA.Close
%>

<table width=100% border="1" cellspacing="0" cellpadding="0" bordercolor=Black>
  <tr>
   <td rowspan=2 bgcolor=silver><b>Cumplimiento de Cuota</b></td>   
   <td colspan=4 align=center><b><%=year(rsFecha("FechaActual"))-1%></b></td>   
  </tr>
  <tr>   
   <td align=center bgcolor=Silver>Ventas</td>
   <td align=center bgcolor=Silver>Margen</td>
   <td align=center bgcolor=Silver>Cuota</td>         
   <td align=center bgcolor=Silver>%</td>
  </tr>
  <tr>   
   <td>Q1</td>   
   <td align=right><%=formatcurrency(round(Q1AnoAnt_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q1AnoAnt_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q1AnoAnt_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(Q1AnoAnt_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(Q1AnoAnt_Margen/1000,0)/round(Q1AnoAnt_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  <tr>    
   <td>Q2</td>   
   <td align=right><%=formatcurrency(round(Q2AnoAnt_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q2AnoAnt_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q2AnoAnt_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(Q2AnoAnt_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(Q2AnoAnt_Margen/1000,0)/round(Q2AnoAnt_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  <tr>      
   <td>Q3</td>
   <td align=right><%=formatcurrency(round(Q3AnoAnt_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q3AnoAnt_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q3AnoAnt_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(Q3AnoAnt_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(Q3AnoAnt_Margen/1000,0)/round(Q3AnoAnt_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  <tr>   
   <td>Q4</td>   
   <td align=right><%=formatcurrency(round(Q4AnoAnt_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q4AnoAnt_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(Q4AnoAnt_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(Q4AnoAnt_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(Q4AnoAnt_Margen/1000,0)/round(Q4AnoAnt_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%>
  </tr>
  <tr>   
   <td><b>Total&nbsp;&nbsp;<%=AnoIni%></b></td>   
   <td align=right><b><%=formatcurrency(round(TotAnoAnt_Ventas/1000,0),0,-1,0)%> K</b></td>
   <td align=right><b><%=formatcurrency(round(TotAnoAnt_Margen/1000,0),0,-1,0)%> K</b></td>
   <td align=right><b><%=formatcurrency(TotAnoAnt_Cuota,0,-1,0)%> K</b></td>
   <%if TotAnoAnt_Cuota=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><b><%=formatnumber(round((round(TotAnoAnt_Margen/1000,0)/TotAnoAnt_Cuota)*100,0),0,-1,0)%> %</b></td>
   <%end if%> 
  </tr>
  
<%SqlSGA = "Exec NAVISION_VtaCtoMar_VenDLLS "
  SqlSGA = SqlSGA & "'" & request("vendedor") & "','" & AnoFin &"'"
  'Response.Write SqlSGA
  rsSGA.Open SqlSGA,connSGA      
  
  Do while not rsSGA.EOF        
    if rsSGA("Año")=AnoFin then
     if rsSGA("Mes")=1 and MesActual>=1 then     
      M1AnoAct_Ventas=M1AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M1AnoAct_Margen=M1AnoAct_Margen+formatCurrency(rsSGA("Margen"))     	  	  
     elseif rsSGA("Mes")=2 and MesActual>=2 then     
      M2AnoAct_Ventas=M2AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M2AnoAct_Margen=M2AnoAct_Margen+formatCurrency(rsSGA("Margen"))      	  
     elseif rsSGA("Mes")=3 and MesActual>=3 then     
      M3AnoAct_Ventas=M3AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M3AnoAct_Margen=M3AnoAct_Margen+formatCurrency(rsSGA("Margen"))      	  
     elseif rsSGA("Mes")=4 and MesActual>=4 then     
      M4AnoAct_Ventas=M4AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M4AnoAct_Margen=M4AnoAct_Margen+formatCurrency(rsSGA("Margen"))      	  
	 elseif rsSGA("Mes")=5 and MesActual>=5 then     
      M5AnoAct_Ventas=M5AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M5AnoAct_Margen=M5AnoAct_Margen+formatCurrency(rsSGA("Margen"))     	  
	 elseif rsSGA("Mes")=6 and MesActual>=6 then     
      M6AnoAct_Ventas=M6AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M6AnoAct_Margen=M6AnoAct_Margen+formatCurrency(rsSGA("Margen"))      	  
	 elseif rsSGA("Mes")=7 and MesActual>=7 then     
      M7AnoAct_Ventas=M7AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M7AnoAct_Margen=M7AnoAct_Margen+formatCurrency(rsSGA("Margen"))       	  
	  elseif rsSGA("Mes")=8 and MesActual>=8 then     
      M8AnoAct_Ventas=M8AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M8AnoAct_Margen=M8AnoAct_Margen+formatCurrency(rsSGA("Margen"))       	  
	  elseif rsSGA("Mes")=9 and MesActual>=9 then     
      M9AnoAct_Ventas=M9AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M9AnoAct_Margen=M9AnoAct_Margen+formatCurrency(rsSGA("Margen"))       	  
	  elseif rsSGA("Mes")=10 and MesActual>=10 then     
      M10AnoAct_Ventas=M10AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M10AnoAct_Margen=M10AnoAct_Margen+formatCurrency(rsSGA("Margen"))      	  
	  elseif rsSGA("Mes")=11 and MesActual>=11 then     
      M11AnoAct_Ventas=M11AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M11AnoAct_Margen=M11AnoAct_Margen+formatCurrency(rsSGA("Margen"))       	  
	  elseif rsSGA("Mes")=12 and MesActual>=12 then     
      M12AnoAct_Ventas=M12AnoAct_Ventas+formatCurrency(rsSGA("Venta"))
	  M12AnoAct_Margen=M12AnoAct_Margen+formatCurrency(rsSGA("Margen"))       	  
     end if
    end if   
    rsSGA.MoveNext
  loop
  
  TotAnoAct_Ventas=round(M1AnoAct_Ventas/1000,0)+round(M2AnoAct_Ventas/1000,0)+round(M3AnoAct_Ventas/1000,0)+round(M4AnoAct_Ventas/1000,0)+round(M5AnoAct_Ventas/1000,0)+round(M6AnoAct_Ventas/1000,0)+round(M7AnoAct_Ventas/1000,0)+round(M8AnoAct_Ventas/1000,0)+round(M9AnoAct_Ventas/1000,0)+round(M10AnoAct_Ventas/1000,0)+round(M11AnoAct_Ventas/1000,0)+round(M12AnoAct_Ventas/1000,0)  
  TotAnoAct_Margen=round(M1AnoAct_Margen/1000,0)+round(M2AnoAct_Margen/1000,0)+round(M3AnoAct_Margen/1000,0)+round(M4AnoAct_Margen/1000,0)+round(M5AnoAct_Margen/1000,0)+round(M6AnoAct_Margen/1000,0)+round(M7AnoAct_Margen/1000,0)+round(M8AnoAct_Margen/1000,0)+round(M9AnoAct_Margen/1000,0)+round(M10AnoAct_Margen/1000,0)+round(M11AnoAct_Margen/1000,0)+round(M12AnoAct_Margen/1000,0)
  
  SqlCuota = "Select * from Tblcuotas"
  SqlCuota = SqlCuota & " Where cveusuario='" & request("vendedor") & "' and ano=" & AnoFin
  SqlCUota = SqlCuota & " Order by Ano,cve_mes"
  'Response.Write SqlCuota
  rsCuota.Open SqlCuota,conn

  Do while not rsCuota.EOF   
  if rsCuota("Ano")=AnoFin then
   if rsCuota("Cve_Mes")=1 then   
	M1AnoAct_Cuota=rsCuota("Monto_Cuota")
	if MesActual>=1 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M1AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=2 then	
	M2AnoAct_Cuota=rsCuota("Monto_Cuota")
	if MesActual>=2 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M2AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=3 then
	M3AnoAct_Cuota=rsCuota("Monto_Cuota")
	if MesActual>=3 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M3AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=4 then
    M4AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=4 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M4AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=5 then
    M5AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=5 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M5AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=6 then
    M6AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=6 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M6AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=7 then 
    M7AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=7 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M7AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=8 then 
    M8AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=8 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M8AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=9 then 
    M9AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=9 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M9AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=10 then 
    M10AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=10 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M10AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=11 then 
    M11AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=11 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M11AnoAct_Cuota/1000,0)
   elseif rsCuota("Cve_Mes")=12 then 
    M12AnoAct_Cuota=rsCuota("Monto_Cuota")
    if MesActual>=12 then TotAnoAct_Cuota=TotAnoAct_Cuota+round(M12AnoAct_Cuota/1000,0)
   end if 
  end if
   
  rsCuota.MoveNext
  Loop  
  
  'TotAnoAct_Cuota=round(M1AnoAct_Cuota/1000,0)+round(M2AnoAct_Cuota/1000,0)+round(M3AnoAct_Cuota/1000,0)+round(M4AnoAct_Cuota/1000,0)+round(M5AnoAct_Cuota/1000,0)+round(M6AnoAct_Cuota/1000,0)
  'TotAnoAct_Cuota=TotAnoAct_Cuota+round(M7AnoAct_Cuota/1000,0)+round(M8AnoAct_Cuota/1000,0)+round(M9AnoAct_Cuota/1000,0)+round(M10AnoAct_Cuota/1000,0)+round(M11AnoAct_Cuota/1000,0)+round(M12AnoAct_Cuota/1000,0)
  
  rsCuota.Close
  
%>  

  <tr>
   <td rowspan=2><br></b></td>   
   <td colspan=4 align=center><b><%=year(rsFecha("FechaActual"))%></b></td>   
  </tr>
  <tr>   
   <td align=center bgcolor=Silver>Ventas</td>
   <td align=center bgcolor=Silver>Margen</td>
   <td align=center bgcolor=Silver>Cuota</td>
   <td align=center bgcolor=Silver>%</td>         
  </tr>
  
  <%if MesActual>=1 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Enero</td>   
   <td align=right><%=formatcurrency(round(M1AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M1AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M1AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M1AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M1AnoAct_Margen/1000,0)/round(M1AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=2 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Febrero</td>   
   <td align=right><%=formatcurrency(round(M2AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M2AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M2AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M2AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M2AnoAct_Margen/1000,0)/round(M2AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=3 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
   <td>Marzo</td>
   <td align=right><%=formatcurrency(round(M3AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M3AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M3AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M3AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M3AnoAct_Margen/1000,0)/round(M3AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=4 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Abril</td>   
   <td align=right><%=formatcurrency(round(M4AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M4AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M4AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M4AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M4AnoAct_Margen/1000,0)/round(M4AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=5 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Mayo</td>   
   <td align=right><%=formatcurrency(round(M5AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M5AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M5AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M5AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M5AnoAct_Margen/1000,0)/round(M5AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=6 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Junio</td>   
   <td align=right><%=formatcurrency(round(M6AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M6AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M6AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M6AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M6AnoAct_Margen/1000,0)/round(M6AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=7 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Julio</td>
   <td align=right><%=formatcurrency(round(M7AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M7AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M7AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M7AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M7AnoAct_Margen/1000,0)/round(M7AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%>
  </tr>
  
  <%if MesActual>=8 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Agosto</td>   
   <td align=right><%=formatcurrency(round(M8AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M8AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M8AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M8AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M8AnoAct_Margen/1000,0)/round(M8AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=9 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Septiembre</td>   
   <td align=right><%=formatcurrency(round(M9AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M9AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M9AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M9AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M9AnoAct_Margen/1000,0)/round(M9AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=10 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Octubre</td>   
   <td align=right><%=formatcurrency(round(M10AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M10AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M10AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M10AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M10AnoAct_Margen/1000,0)/round(M10AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
  
  <%if MesActual>=11 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Noviembre</td>
   <td align=right><%=formatcurrency(round(M11AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M11AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M11AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M11AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M11oAct_Margen/1000,0)/round(M11AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>
 
 <%if MesActual>=12 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Diciembre</td>   
   <td align=right><%=formatcurrency(round(M12AnoAct_Ventas/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M12AnoAct_Margen/1000,0),0,-1,0)%> K</td>
   <td align=right><%=formatcurrency(round(M12AnoAct_Cuota/1000,0),0,-1,0)%> K</td>
   <%if round(M12AnoAct_Cuota/1000,0)=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><%=formatnumber(round((round(M12AnoAct_Margen/1000,0)/round(M12AnoAct_Cuota/1000,0))*100,0),0,-1,0)%> %</td>
   <%end if%> 
  </tr>  
 
  <tr>   
   <td><b>Total Actual&nbsp;&nbsp;<%=AnoFin%></b></td>   
   <td align=right><b><%=formatcurrency(TotAnoAct_Ventas,0,-1,0)%> K</b></td>
   <td align=right><b><%=formatcurrency(TotAnoAct_Margen,0,-1,0)%> K</b></td>
   <td align=right><b><%=formatcurrency(TotAnoAct_Cuota,0,-1,0)%> K</b></td>
   <%if TotAnoAct_Cuota=0 then%>
    <td><br></td>
   <%else%> 
    <td align=right><b><%=formatnumber(round((TotAnoAct_Margen/TotAnoAct_Cuota)*100,0),0,-1,0)%> %</b></td>
   <%end if%> 
  </tr>  
  </table>  
 </td> 
<%end if%>  
 <td align=right valign=top>
<% Q1AnoAnt_Visitas=0   
   Q2AnoAnt_Visitas=0   
   Q3AnoAnt_Visitas=0
   Q4AnoAnt_Visitas=0   
   
   M1AnoAnt_Visitas=0
   M2AnoAnt_Visitas=0
   M3AnoAnt_Visitas=0
   M4AnoAnt_Visitas=0
   M5AnoAnt_Visitas=0
   M6AnoAnt_Visitas=0
   M7AnoAnt_Visitas=0
   M8AnoAnt_Visitas=0
   M9AnoAnt_Visitas=0
   M10AnoAnt_Visitas=0
   M11AnoAnt_Visitas=0
   M12AnoAnt_Visitas=0
   TotAnoAnt_Visitas=0
   
   M1AnoAct_Visitas=0
   M2AnoAct_Visitas=0
   M3AnoAct_Visitas=0
   M4AnoAct_Visitas=0
   M5AnoAct_Visitas=0
   M6AnoAct_Visitas=0
   M7AnoAct_Visitas=0
   M8AnoAct_Visitas=0
   M9AnoAct_Visitas=0
   M10AnoAct_Visitas=0
   M11AnoAct_Visitas=0
   M12AnoAct_Visitas=0
   TotAnoAct_Visitas=0
%>

<%
  SqlVisitas = "Exec PerformanceComercial_Visitas "
  SqlVisitas = SqlVisitas & "'" & request("vendedor") & "','" & AnoIni &"'"    
  'Response.Write SqlVisitas
  rs.Open SqlVisitas,conn
  
    Do while not rs.EOF        
    if rs("Ano")=AnoIni then
     if rs("Mes")=1 then     
      M1AnoAnt_Visitas=M1AnoAnt_Visitas+rs("Visitas")	  
     elseif rs("Mes")=2 then     
      M2AnoAnt_Visitas=M2AnoAnt_Visitas+rs("Visitas")	  
     elseif rs("Mes")=3 then     
      M3AnoAnt_Visitas=M3AnoAnt_Visitas+rs("Visitas")	  
     elseif rs("Mes")=4 then     
      M4AnoAnt_Visitas=M4AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=5 then     
      M5AnoAnt_Visitas=M5AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=6 then     
      M6AnoAnt_Visitas=M6AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=7 then     
      M7AnoAnt_Visitas=M7AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=8 then     
      M8AnoAnt_Visitas=M8AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=9 then     
      M9AnoAnt_Visitas=M9AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=10 then     
      M10AnoAnt_Visitas=M10AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=11 then     
      M11AnoAnt_Visitas=M11AnoAnt_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=12 then     
      M12AnoAnt_Visitas=M12AnoAnt_Visitas+rs("Visitas")	  
     end if
    end if   
    rs.MoveNext
  loop
  
  Q1AnoAnt_Visitas=M1AnoAnt_Visitas+M2AnoAnt_Visitas+M3AnoAnt_Visitas
  Q2AnoAnt_Visitas=M4AnoAnt_Visitas+M5AnoAnt_Visitas+M6AnoAnt_Visitas
  Q3AnoAnt_Visitas=M7AnoAnt_Visitas+M8AnoAnt_Visitas+M9AnoAnt_Visitas
  Q4AnoAnt_Visitas=M10AnoAnt_Visitas+M11AnoAnt_Visitas+M12AnoAnt_Visitas
  
  TotAnoAnt_Visitas=Q1AnoAnt_Visitas+Q2AnoAnt_Visitas+Q3AnoAnt_Visitas+Q4AnoAnt_Visitas
  
  rs.Close
%>

<table width=70% border="1" cellspacing="0" cellpadding="0" bordercolor=Black>
  <tr>
   <td rowspan=2 bgcolor=silver><b>Visitas</b></td>   
   <td  colspan=4 align=center><b><%=year(rsFecha("FechaActual"))-1%></b></td>   
  </tr>
  <tr>   
   <td align=center bgcolor=Silver>Visitas</td>   
  </tr>
  <tr>   
   <td>Q1</td>   
   <td align=right><%=Q1AnoAnt_Visitas%></td>   
  </tr>
  <tr>    
   <td>Q2</td>   
   <td align=right><%=Q2AnoAnt_Visitas%></td>   
  </tr>
  <tr>      
   <td>Q3</td>
   <td align=right><%=Q3AnoAnt_Visitas%></td>
  </tr>
  <tr>   
   <td>Q4</td>   
   <td align=right><%=Q4AnoAnt_Visitas%></td>   
  </tr>
  <tr>   
   <td><b>Total&nbsp;&nbsp;<%=AnoIni%></b></td>   
   <td align=right><b><%=TotAnoAnt_Visitas%></b></td>   
  </tr>
  <%
  SqlVisitas = "Exec PerformanceComercial_Visitas "
  SqlVisitas = SqlVisitas & "'" & request("vendedor") & "','" & AnoFin &"'"    
  'Response.Write SqlVisitas
  rs.Open SqlVisitas,conn
  
    Do while not rs.EOF        
    if rs("Ano")=AnoFin then
     if rs("Mes")=1 and MesActual>=1 then     
      M1AnoAct_Visitas=M1AnoAct_Visitas+rs("Visitas")	  
     elseif rs("Mes")=2 and MesActual>=2 then     
      M2AnoAct_Visitas=M2AnoAct_Visitas+rs("Visitas")	  
     elseif rs("Mes")=3 and MesActual>=3 then     
      M3AnoAct_Visitas=M3AnoAct_Visitas+rs("Visitas")	  
     elseif rs("Mes")=4 and MesActual>=4 then     
      M4AnoAct_Visitas=M4AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=5 and MesActual>=5 then     
      M5AnoAct_Visitas=M5AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=6 and MesActual>=6 then     
      M6AnoAct_Visitas=M6AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=7 and MesActual>=7 then     
      M7AnoAct_Visitas=M7AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=8 and MesActual>=8 then     
      M8AnoAct_Visitas=M8AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=9 and MesActual>=9 then     
      M9AnoAct_Visitas=M9AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=10 and MesActual>=10 then     
      M10AnoAct_Visitas=M10AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=11 and MesActual>=11 then     
      M11AnoAct_Visitas=M11AnoAct_Visitas+rs("Visitas")	  
	 elseif rs("Mes")=12 and MesActual>=12 then     
      M12AnoAct_Visitas=M12AnoAct_Visitas+rs("Visitas")	  
     end if
    end if   
    rs.MoveNext
  loop    
  
  TotAnoAct_Visitas=M1AnoAct_Visitas+M2AnoAct_Visitas+M3AnoAct_Visitas
  TotAnoAct_Visitas=TotAnoAct_Visitas+M4AnoAct_Visitas+M5AnoAct_Visitas+M6AnoAct_Visitas
  TotAnoAct_Visitas=TotAnoAct_Visitas+M7AnoAct_Visitas+M8AnoAct_Visitas+M9AnoAct_Visitas
  TotAnoAct_Visitas=TotAnoAct_Visitas+M10AnoAct_Visitas+M11AnoAct_Visitas+M12AnoAct_Visitas    
  
  rs.close
%>
<tr>
   <td rowspan=2><br></b></td>   
   <td colspan=4 align=center><b><%=year(rsFecha("FechaActual"))%></b></td>   
  </tr>
  <tr>   
   <td align=center bgcolor=Silver>Visitas</td>   
  </tr>
  <%if MesActual>=1 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
 
   <td>Enero</td>   
   <td align=right><%=M1AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=2 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Febrero</td>   
   <td align=right><%=M2AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=3 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Marzo</td>
   <td align=right><%=M3AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=4 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Abril</td>   
   <td align=right><%=M4AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=5 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Mayo</td>   
   <td align=right><%=M5AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=6 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Junio</td>   
   <td align=right><%=M6AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=7 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
        
   <td>Julio</td>
   <td align=right><%=M7AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=8 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Agosto</td>   
   <td align=right><%=M8AnoAct_Visitas%></td>   
  </tr>
  
  <%if MesActual>=9 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Septiembre</td>   
   <td align=right><%=M9AnoAct_Visitas%></td>   
  </tr>
 
 <%if MesActual>=10 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
 
   <td>Octubre</td>   
   <td align=right><%=M10AnoAct_Visitas%></td>   
  </tr>
 
  <%if MesActual>=11 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Noviembre</td>
   <td align=right><%=M11AnoAct_Visitas%></td>   
  </tr>
 
 <%if MesActual>=12 then%>
    <tr>
  <%else%> 
    <tr  bgcolor=Lavender>      
  <%end if%>  
  
   <td>Diciembre</td>   
   <td align=right><%=M12AnoAct_Visitas%></td>   
  </tr>  
 
  <tr>   
   <td><b>Total Actual&nbsp;&nbsp;<%=AnoFin%></b></td>   
   <td align=right><b><%=TotAnoAct_Visitas%></b></td>   
  </tr>  
</table>

</td>
</tr>
</table>

<br>

<%
  SqlSuf = "Exec PerformanceComercial_SuficienciaOP "
  SqlSuf = SqlSuf & "'" & request("vendedor") & "'"   
  'Response.Write SqlSuf
  rs.Open SqlSuf,conn
  
  Eficacia=rs("Eficacia")
  CuotaMensualPromedio=rs("CuotaMensualPromedio")
  ValorRealizacionInv=(TotInvent_Margen*round(Eficacia,1))/100  
  MesesCuota=round(ValorRealizacionInv/30,1)
  DiasPromCierre=rs("DiasPromCierre")
  DiasPromFactur=rs("DiasPromFactur")
  if round(Eficacia,1)=0 then
   MargenReqForecast=0
  else 
   MargenReqForecast=((((round(DiasPromCierre,0)+round(DiasPromFactur,0))/30)*round(CuotaMensualPromedio/1000,0))/round(Eficacia,1))*100 
  end if 
  SobranteFaltante=TotInvent_Margen-MargenReqForecast
  rs.Close
%>


<table width=40% border="1" cellspacing="0" cellpadding="0" bordercolor=Black>
  <tr>
   <td height=30 colspan=2 bgcolor=silver><b>Suficiencia de Inventario de Oportunidades</b></td>      
  </tr>
  <tr>   
   <td>Eficacia en Venta</td> 
   <td align=right><%=formatnumber(round(Eficacia,1),1,-1,0)%> %</td>
  </tr> 
  <tr>   
   <td>Cuota Margen Mensual Promedio</td> 
   <td align=right><%=formatcurrency(round(CuotaMensualPromedio/1000,0),0,-1,0)%> K</td>
  </tr> 
  <tr>   
   <td>Tiempo Estándar de Cierre</td> 
   <td align=right><%=formatnumber(round(DiasPromCierre,0),0,-1,0)%> Días</td>
  </tr> 
  <tr>   
   <td>Tiempo Facturación Promedio</td> 
   <td align=right><%=formatnumber(round(DiasPromFactur,0),0,-1,0)%> Días</td>
  </tr> 
  <tr>   
   <td>Margen Requerido en Forecast</td> 
   <td align=right><%=formatcurrency(MargenReqForecast,0,-1,0)%> K</td>
  </tr> 
  <tr>   
   <td>Valor Actual Margen Op</td> 
   <td align=right><%=formatcurrency(TotInvent_Margen,0,-1,0)%> K</td>
  </tr> 
  <tr bgcolor=silver>   
   <td><b>Sobrante/Faltante</b></td> 
   <td align=right><%=formatcurrency(SobranteFaltante,0,-1,0)%> K</td>
  </tr> 
  <tr>   
   <td>Valor Realización Inventario Op</td> 
   <td align=right><%=formatcurrency(ValorRealizacionInv,0,-1,0)%> K</td>
  </tr> 
  <tr>   
   <td>Meses Cuota</td> 
   <td align=right><%=formatnumber(MesesCuota,1,-1,0)%></td>
  </tr> 
 </table>

 <br>

<%
  SqlDetalle = "Exec PerformanceComercial_DetalleInventarioOP "
  SqlDetalle = SqlDetalle & "'" & request("vendedor") & "'"   
  'Response.Write SqlDetalle
  rs.Open SqlDetalle,conn
%>


<table width=70% border="1" cellspacing="0" cellpadding="0" bordercolor=Black>
  <tr>
   <td height=30 colspan=3 width=60% bgcolor=silver><b>Detalle de Inventario de Oportunidades</b></td>      
  </tr>
  <tr bgcolor=silver>   
   <td align=center>Cliente</td>   
   <td align=center>Margen</td>
   <td align=center>Op's</td>
  </tr> 
  <% Do while not rs.EOF 
    TotMargenOP=TotMargenOP+round(rs("MargenOP")/1000,0)
    TotNumOP=TotNumOP+rs("NumOP")
  %>  
   <tr>
    <td><%=rs("NombCli")%></td>
    <td align=right><%=formatcurrency(round(rs("MargenOP")/1000,0),0,-1,0)%> K</td>
    <td align=right><%=rs("NumOP")%></td>
   </tr>   
  <% rs.MoveNext
     Loop
  %>  
  <tr>
   <td><b>Total</b></td>
   <td align=right><b><%=formatcurrency(TotMargenOP,0,-1,0)%> K</b></td>
   <td align=right><b><%=formatnumber(TotNumOP,0,-1,0)%></b></td>
  </tr>
 </table>
</BODY>
</HTML>



<% rs.Close%>
<% rsVen.Close%>
<% rsFecha.Close%>



<%set rs = nothing %>
<%set rsFecha = nothing%>
<%set rsVen = nothing%>
<%if Application("ConeccionERP")=1 then%>
  <%set rsSGA = nothing%>
<%end if%>
<%set rsCuota = nothing%>

