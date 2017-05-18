<xsl:stylesheet
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      version="1.0">
  <xsl:output method="xml"/>

  <xsl:template match="/">
    <xsl:for-each select="html/hr/p/em/a">
      <quotation>
        <number>
          <xsl:value-of select="@name"/>
        </number>
        <xsl:apply-templates/>
      </quotation>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="/small/font">
    <reference>
      <xsl:value-of select="."/>
    </reference>
  </xsl:template>

  <xsl:template match="blockquote/p/small/font">
    <narrator>
      <xsl:value-of select="."/>
    </narrator>
  </xsl:template>

  <xsl:template match="p/small/font">
    <text>
      <xsl:value-of select="."/>
    </text>
  </xsl:template>

  <xsl:template match="p">
    <xsl:for-each select="p/small/font">
      <xsl:text></xsl:text>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
