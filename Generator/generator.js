$(function() 
{
	var recording = false, playing = false;
	var start = 0;
	var d, t;
	var n1, n2, n3;
	var notes;
	var song;
	var fired1 = false;
	var fired2 = false;
	var fired3 = false;

	$("#start").click(function(){
		recording = !recording;
		d = new Date(); t = d.getTime();
		if(recording)
		{			
			song = new Song();
			notes = [];
			start = t;
			$("#input").html("");
		}
		else
		{
			$("#input").append(JSON.stringify(song)+"<br/>");
		}
		if(recording)
			$(this).text("Stop");
		else
			$(this).text("Start");
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
		}
	});

	$("#playback").click(function(){
		playing = !playing;
		d = new Date(); t = d.getTime();
		if(playing)
		{			
			var json = $("#input").val();
			song = JSON.parse(json);
			prepare(song.A, "A");
			prepare(song.B, "B");
			prepare(song.C, "C");
		}
		else
		{
		}
		if(playing)
			$(this).text("Stop");
		else
			$(this).text("Playback");
	});
});

function prepare(line, which)
{
	for (var i=0;i<line.length;i++)
	{ 
		(function(a,b,c){
     		setTimeout(function(){
            	play(a,b)}, c);
      	})(which, line[i].length, line[i].start);
	}
}

function play(which, length)
{
	$("#res"+which).addClass("active"+which);
	setTimeout(function(){
		$("#res"+which).removeClass("active"+which);
	}, length);
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