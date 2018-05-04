$(document).ready(function () {

    $("#btn").click(function () {

        var x = document.getElementsByClassName("champs-competence").length;

        var html = "<div class='champs-competence'><div class='form-group'><label class='control-label'> Compétence</label > <div><textarea name='[" + x + "].Competence' cols='60' rows='1' class='form-control'></textarea></div></div > <div class='form-group'><label class='control-label'>Niveau</label><div><select class='form-control text-center' name='[" + x + "].NiveauCompetence'><option value=''> - Niveau - </option><option value='Debutant'> Debutant (0 à 1 an) </option><option value='Confirme'>Confirmé (2 à 5 ans)</option><option value='Experimente'>Expérimenté (plus de 5 ans)</option ></select ></div ></div ></div > ";
        $("#d").append(html);
    });
});


$(document).ready(function () {

    $("#btnAjouterLangue").click(function () {

        var x = document.getElementsByClassName("Champ_langue").length;

        var html = '<div class="Champ_langue"><div class="form-group"><label>Autre</label><div><input class="form-control" name="[' + x + '].Langue" style="width=48px;"/></div></div><div class="form-group"><label class="control-label">Niveau Langue</label><select style="text-align-last:center" class="form-control" name="[' + x + '].NiveauLangue"><option value="Non opérationnel"> Non opérationnel </option><option value="Opérationnel">Opérationnel</option><option value="Courant">Courant</option></select></div></div>';
        $("#l").append(html);
    });
});


$(document).ready(function () {

    $("#btn1").click(function () {
        var y = document.getElementsByClassName("champs").length;
        var html = "<div class='champs'><div class='form-group'><label class='control-label'>Nom/Prénom</label><div><input name='[" + y + "].NomPrenom' class='form-control'/></div></div><div class='form-group'><label class='control-label'>Fonction</label><div><input name='[" + y + "].Fonction' class='form-control'/></div></div><div class='form-group'><label class='control-label'>Sociéte</label><div><input name='[" + y + "].Societe' class='form-control'/></div></div><div class='form-group'><label class='control-label'>Tel/Mail</label><div><input name='[" + y + "].TelMail' class='form-control'/></div></div></div>";
        $("#d1").append(html);
    });
});


$(document).ready(function () {

    $("#origine").change(function () {
        //var y = document.getElementById("select").firstChild.nodeValue;
        var t = document.getElementById("origine");
        var indexSelect = t.selectedIndex;
        if (indexSelect == 4) {
            document.getElementById("Autre").style.display = "block";
        }
        else
            document.getElementById("Autre").style.display = "none";
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
        else {
            document.getElementById("posteActuel").style.display = "block";
            document.getElementById("remunerationActuel").style.display = "block";
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
            document.getElementById("inputMobilitePrecision").value = '';

        }

    });
});
$(document).ready(function () {

    $("#AutorisationTravail").click(function () {

        var t = document.querySelector('input[id="AutorisationTravail"]');

        if (t.checked) {

            document.getElementById("inputDateAutorisation").style.display = 'block';
        }
        if (!t.checked) {

            document.getElementById("inputDateAutorisation").style.display = 'none';
            document.getElementById("inputDateAutorisation").value = '';

        }

    });
});

