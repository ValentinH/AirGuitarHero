var song;
var total_length=0;	
var recording = false;
var start = 0;
var d, t;
var n1, n2, n3;
var notes;
var song;
var fired1 = false;
var fired2 = false;
var fired3 = false;

$(function() 
{
	$("#song").hide();
	$("#save-zone").hide();
	$("#feedback").hide();

	if (window.File) 
	{
		$("#file").change(function(){
			var file = this.files[0];
			var textType = /text.*/;
			if (file.type.match(textType)) {
				var reader = new FileReader();

				reader.onload = function(e) {
					song = JSON.parse(reader.result);
					displayNotes();
				}

				reader.readAsText(file);  
			}
		});
	} 
	else
		alert('The File APIs are not fully supported in this browser.');

	$("#save").click(save);
	$("#save_under").click(saveUnder);

	$("#start").click(function(){
		recording = !recording;
		d = new Date(); t = d.getTime();
		if(recording)
		{			
			song = new Song();
			notes = [];
			start = t;
			total_length = 0;
			$("#input").html("");
			$(this).text("Stop");
			$("#feedback").show();
			$("#song").hide();
			$("#save-zone").hide();
		}
		else
		{
			displayNotes();
			$("#feedback").hide();
			$(this).text("Start a new song");
		}
	});

	$( "body" ).keydown(function(e) {
		if(recording)
		{
			d = new Date(); t = d.getTime();
			switch(e.keyCode)
			{
				case 37:
				if(!fired1)
				{
					fired1 = true;
					n1 = t-start;
					$("#A").addClass("activeA");
				}
				break;
				case 40:
				if(!fired2)
				{
					fired2 = true;
					n2 = t-start;
					$("#B").addClass("activeB");
				}
				break;
				case 39:
				if(!fired3)
				{
					fired3 = true;
					n3 = t-start;
					$("#C").addClass("activeC");
				}
				break;
			}
		}
	});
	$( "body" ).keyup(function(e) {
		if(recording)
		{			
			d = new Date(); t = d.getTime();
			switch(e.keyCode)
			{
				case 37:
				fired1 = false;
				song.A.push(new Note(n1, (t-start-n1)));
				$("#A").removeClass("activeA");
				break;
				case 40:
				fired2 = false;
				song.B.push(new Note(n2, (t-start-n2)));
				$("#B").removeClass("activeB");
				break;
				case 39:
				fired3 = false;
				song.C.push(new Note(n3, (t-start-n3)));
				$("#C").removeClass("activeC");
				break;
			}
		// S
		if(e.keyCode == 83)
			$("#start").click();
		}
	});
});

function addHandler(el)
{
	$(el).resizable({
		grid: [20,20],
		handles: "n, s",
		containment: "parent",
	});
	$(el).on( "dragstop", function( event, ui ) {
		setNoteTitle(el, ui.position.top, $(this).height());
	} );
	$(el).on( "resizestop", function( event, ui ) {
		setNoteTitle(el, ui.position.top, ui.size.height);
	} );
	$(el).draggable({ containment: "parent", axis: "y",  grid: [ 20,20 ]});
	$(el).bind('contextmenu', function(e) {
		$(this).remove();
		return false;
	});	
}

function setNoteTitle(el, start, length)
{
	$(el).attr("title","start: "+(start*10)+", length: "+(length*10));
}

function displayNotes()
{
	displayPiste(song.A, "A");
	displayPiste(song.B, "B");
	displayPiste(song.C, "C");

	$("#song").css({height: round(100,total_length) +100});
	$("#song").show();	
	$("#save-zone").show();
	displayTime();
	addHandler(".note");
	
}

function displayPiste(p, which)
{
	$("#piste"+which).html("");
	for (var i=0;i<p.length;i++)
	{
		var h = p[i].length/10;
		var t = p[i].start/10;
		if(total_length< h+t)
			total_length = h+t;
		var newNote = $("<div>", {class: "note"});		
		setNoteTitle(newNote, t, h);
		newNote.css({height: h, top: t});
		$("#piste"+which).append(newNote);
	}
	$("#piste"+which).dblclick(function(event) {
		var h = 20;
		var t = round(20, event.offsetY);
		var newNote = $("<div>", {class: "note"});
		setNoteTitle(newNote, t, 20);
		newNote.css({height: h, top: t});
		$("#piste"+which).append(newNote);
		addHandler(newNote);

		if(h+t+50>$("#pistes").height())
		{
			$("#pistes").height($("#pistes").height()+100);	
			displayTime()
		}
	});
}
function displayTime()
{
	$("#time").html("");
	for (var i=0;i<=$("#pistes").height()/100;i++)
	{
		var d = new Date(i*1000)
		var newTime = $("<div>", {class: "time-item"});
		newTime.html(checkTime(d.getMinutes())+":"+checkTime(d.getSeconds())+" ___________________________");
		$("#time").append(newTime);
	}
}

function checkTime(i)
{
	if (i<10)
	{
		i="0" + i;
	}
	return i;
}
function save()
{
	song = new Song();
	$("#pisteA .note").each(function(){
		song.A.push(new Note( $(this).position().top*10,  $(this).height()*10));
	});
	$("#pisteB .note").each(function(){
		song.B.push(new Note( $(this).position().top*10,  $(this).height()*10));
	});
	$("#pisteC .note").each(function(){
		song.C.push(new Note( $(this).position().top*10,  $(this).height()*10));
	});

	song.A.sort(SortByStart);
	song.B.sort(SortByStart);
	song.C.sort(SortByStart);

	var json = JSON.stringify(song);
	$("#output").append(json);
	$('#myModal').modal();
	}

function saveUnder()
{
	var json = JSON.stringify(song);
	var text_filename = $("#filename")[0];
	saveAs(new Blob([json], {type: "text/plain;charset=" + document.characterSet}),(text_filename.value || text_filename.placeholder) + ".txt");

}

function Song() {
	this.A = [];
	this.B = [];
	this.C = [];
}

function Note(start, length) {
	this.start = round(200, start);
	this.length = round(200, length);
}

function round(r, n)
{
	var mod = n%r;
	if(mod<r/2) n = n -mod;
	else n = n + (r-mod);
	if(n==0) n = r;
	return n;
}

function SortByStart(a, b){
	return ((a.start < b.start) ? -1 : ((a.start > b.start) ? 1 : 0));
}