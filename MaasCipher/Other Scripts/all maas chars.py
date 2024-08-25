
with open(r"F:\New Computer\Unity\MaasCipher\TemplateZIP\HTML\Maas Cipher.html", 'w', encoding='utf-8') as f:
    f.write("""
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="initial-scale=1">
    <title>Maas Cipher — Keep Talking and Nobody Explodes Module</title>
    <link rel="stylesheet" type="text/css" href="css/font.css">
    <link rel="stylesheet" type="text/css" href="css/normalize.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">
    <script src="js/ktane-utils.js"></script>
    <style>
        table {
            margin: 1em auto;
        }
    </style>
</head>
<body>
    <div class="section">
        <div class="page page-bg-01">
            <div class="page-header">
                <span class="page-header-doc-title">Keep Talking and Nobody Explodes Mod</span>
                <span class="page-header-section-title">Maas Cipher</span>
            </div>
            <div class="page-content">
                
            """)
    f.write("<table>\n")
    f.write("<tr><th></th><th>0</th><th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>A</th><th>B</th><th>C</th><th>D</th><th>E</th><th>F</th></tr>\n")

    text = ""
    for i in range(0x61, 0x7b):
        text += "<tr>"
        text += "<th>U+" + hex(i)[2:] + "6x</th>"
        for j in range(0x60, 0x80):
            #chr(i), chr(j), ": "
            if j == 0x70:
                text += "</tr>\n<tr><th>U+" + hex(i)[2:] + "7x</th>"
            text += "<td>" + chr(int(hex(i)+hex(j)[2:], 16)) + "</td>"

        text += "</tr>\n"
        text += "<tr><td></td></tr>\n"


    f.write(text)
    f.write("</table>\n")
    f.write("""
            </div>
            <div class="page-footer relative-footer">Page 1 of 2</div>
        </div>
    </div>
</body>
</html>
            """)