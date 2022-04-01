<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">


  <!-- Determino que el formato se aplica a todo el documento-->
  <xsl:template match="/">

    <!--Creo una tabla para poder desplegar prolijamente-->
    <table >

      <!--Cabezales de las columnas-->
      <tr style ="background-color: #C0C0C0">
        <td style=" border: thin double #800000"> Hora </td>
        <td style=" border: thin double #800000"> Tipo Cielo </td>
        <td style=" border: thin double #800000"> Temp Max </td>
        <td style=" border: thin double #800000"> Temp Min </td>
        <td style=" border: thin double #800000"> Prob. Lluvia </td>
        <td style=" border: thin double #800000"> Prob. Tormenta </td>
        <td style=" border: thin double #800000"> Viento </td>
        
      </tr>

      <!--Determino como quiero desplegar cada nodo -->
      <xsl:for-each select="Raiz/Pronosticos/Pronostico_Hora">
        <tr>
          <td>
            <xsl:value-of select="Hora_pronostico" />
          </td>
          <td>
            <xsl:value-of select="Tipo_Cielo" />
          </td>
          <td>
            <xsl:value-of select="Temp_Max" />
          </td>
          <td style="background-color: #CCFFFF">
            <xsl:value-of select="Temp_Min" />
          </td>
          <td style="background-color: #FFFF99">
            <xsl:value-of select="Probabilidad_Lluvias" />
          </td>
          <td style="background-color: #FFFF99">
            <xsl:value-of select="Probabilidad_Tormentas" />
          </td>
          <td style="background-color: #FFFF99">
            <xsl:value-of select="Velocidad_Viento" />
          </td>

        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>