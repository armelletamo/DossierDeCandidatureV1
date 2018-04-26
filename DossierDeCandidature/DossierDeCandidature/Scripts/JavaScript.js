

$(document).ready(function () {

    $("#btn").click(function () {

        var x = document.getElementsByClassName("champs-competence").length;

        var html = "<div class='champs-competence'><div class='form-group'>< label class='control-label col-md-2' > Compétence</label > <div class='col-md-10' id='d'><textarea name='[" + x + "].Competence' cols='60' rows='2' class='form-control'></textarea></div></div > <div class='form-group'><label class='control-label col-md-2'>Niveau</label><div class='col-md-10'><select class='form-control text-center' name='[" + x + "].NiveauCompetence'><option value=''> - Niveau - </option><option value='Debutant'> Debutant (0 à 1 an) </option><option value='Confirme'>Confirmé (2 à 5 ans)</option><option value='Experimente'>Expérimenté (plus de 5 ans)</option ></select ></div ></div ></div > ";
        $("#d").append(html);
    });
});

$(document).ready(function () {

    $("#btnAjouterLangue").click(function () {

        var x = document.getElementsByClassName("Champ_langue").length;

        var html = '<div class="Champ_langue"><div class="form-group" style="display:inline-block" ><label class="control-label col-md-3">Autre</label><div class="col-md-9"><input type="text" name="[' + x +'].Autre" class="form-control" /></div></div > <div class="form-group" style="display:inline-block"><label class="control-label col-md-3">Niveau  </label><div class="col-md-9"><select class="form-control" name="['+x+'].Niveau"><option value="Non opérationnel"> Non opérationnel </option><option value="Opérationnel">Opérationnel</option><option value="Courant">Courant</option></select></div></div></div >';
        $("#l").append(html);
    });
});

$(document).ready(function () {

    $("#btn1").click(function () {
        var y = document.getElementsByClassName("champs").length;
        var html = "<div class='champs'><div class='form-group'><label class='control-label col-md-2'>Nom/Prénom</label><div class='col-md-10'><input name='[" + y + "].NomPrenom' class='form-control'/></div></div><div class='form-group'><label class='control-label col-md-2'>Fonction</label><div class='col-md-10'><input name='[" + y + "].Fonction' class='form-control'/></div></div><div class='form-group'><label class='control-label col-md-2'>Sociéte</label><div class='col-md-10'><input name='[" + y + "].Societe' class='form-control'/></div></div><div class='form-group'><label class='control-label col-md-2'>Tel/Mail</label><div class='col-md-10'><input name='[" + y + "].TelMail' class='form-control'/></div></div></div>";
        $("#d1").append(html);
    });
});

$(document).ready(function () {

    $("#origine").change(function () {
        //var y = document.getElementById("select").firstChild.nodeValue;
        var t = document.getElementById("origine");
        var indexSelect = t.selectedIndex;
        if (indexSelect == 4)
        {
            document.getElementById("Autre").style.display = "block";
        }
    });
});

$(document).ready(function () {

    $("#statut").change(function () {
        //var y = document.getElementById("select").firstChild.nodeValue;
        var t = document.getElementById("statut");
        var indexSelect = t.selectedIndex;
        if (indexSelect == 5) {
            document.getElementById("posteActuel").style.display = "none";
            document.getElementById("remunerationActuel").style.display = "none";
        }
    });
});

$(document).ready(function () {

    $("#checkboxMobilite").click(function () {
        
        var t = document.querySelector('input[id="checkboxMobilite"]');
        
        if (t.checked) {
            
            document.getElementById("inputMobilitePrecision").style.display = 'block';
        }
        if (!t.checked) {
            
            document.getElementById("inputMobilitePrecision").style.display = 'none';         
           
        }
        
    });
});


//$(document).ready(function () {
   
//    $('#cmd').click(function () {
//        var doc = new jsPDF('p', 'mm', 'letter');       
//        var specialElementHandlers = {
//                return true;
//            '#editor': function (element, renderer) {
//            }
//        };
//        margins = {
//            top: 20,
//            left: 20,
//            rigth: 10,
//            width: 180
//        };
//        doc.fromHTML($('#content')[0]
//            , margins.left 
//            //, margins.rigth
//            , margins.top, {
//                'width': margins.width,
//        'elementHandlers': specialElementHandlers
//            });
//        doc.addPage();
//        doc.fromHTML($('#content1')[0]
//            , margins.left
//            //, margins.rigth
//            , margins.top, {
//                'width': margins.width,
//                'elementHandlers': specialElementHandlers
//            });
//        doc.addPage();
//        doc.fromHTML($('#content2')[0]
//            , margins.left
//            //, margins.rigth
//            , margins.top, {
//                'width': margins.width,
//                'elementHandlers': specialElementHandlers
//            });
//        doc.addPage();
//        doc.fromHTML($('#content3')[0]
//            , margins.left
//            //, margins.rigth
//            , margins.top, {
//                'width': margins.width,
//                'elementHandlers': specialElementHandlers
//            });

//            doc.save('Dossier-de-condidature.pdf');
        
//    });
//});

$(document).ready(function () {
    $("#cmd").click(function () {
        var doc = new jsPDF('p', 'pt', 'a4'),
            source = $("#content")[0],
            margins = {
                top: 70,
                bottom: 30,
                left: 20,
                width: 500
            };
        doc.fromHTML(
            source, // HTML string or DOM elem ref.
            margins.left, // x coord
            margins.top, {
                // y coord
                width: margins.width // max width of content on PDF
            },
            function (dispose) {
                // dispose: object with X, Y of the last line add to the PDF
                //          this allow the insertion of new lines after html
                doc.save("Dossier-de-condidature.pdf");
            },
            margins
        );
    });
});