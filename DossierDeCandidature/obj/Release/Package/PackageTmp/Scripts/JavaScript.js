

    $(document).ready(function () {

        $("#btn").click(function () {

            var x = document.getElementsByClassName("champs-competence").length;
            
            var html = "<div class='champs-competence'><div class='form-group'><label class='control-label col-md-2'>Compétence</label ><div class='col-md-10' id='d'><textarea name='[" + x + "].Competence' cols='60' rows='2' class='form-control'></textarea></div></div ><div class='form-group'><label class='control-label col-md-2'>Niveau</label><div class='col-md-10'><select class='form-control text-center' name='[" + x + "].NiveauCompetence'><option value=''> - Niveau - </option><option value='Debutant'> Debutant (0 à 1 an) </option><option value='Confirme'>Confirmé (2 à 5 ans)</option><option value='Experimente'>Expérimenté (plus de 5 ans)</option ></select ></div ></div ></div>";
            $("#d").append(html);
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
    var doc = new jsPDF();
    var requiredPages = 4
    for (var i = 0; i < requiredPages; i++) {
        doc.addPage();
        //doc.text(20, 100, 'Some Text.');
    }
    //pageHeight = doc.internal.pageSize.height;

    //// Before adding new content
    //y = 500 // Height position of new content
    //if (y >= pageHeight) {
    //    doc.addPage();
    //    y = 0 // Restart height position
    //}
    //doc.text(x, y, "value");

    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };
$('#cmd').click(function () {   
    doc.fromHTML($('#content').html() + $('#content1').html() + $('#content2').html() + $('#content3').html(), 15, 15, {
        'width': 170,
        'elementHandlers': specialElementHandlers
    });
    doc.save('Dossier-de-condidature.pdf');
    });
});

  