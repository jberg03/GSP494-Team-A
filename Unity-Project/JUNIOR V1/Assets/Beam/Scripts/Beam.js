//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com	

#pragma strict
private var counter:float = 2;
private var scaleBuffer:Vector3;
private var beam:Transform;

var startDelay:float;
var pauseDelay:float = 2;
var beamDuration:float = 3; //Negative = beam always on
var animationSpeed:float = 2;
var xMultiplier:float = 0;
var yMultiplier:float = 1;
var particles:ParticleSystem[];	//Place particle systems to show / hide
private var playAudio:boolean;

function Start () {
	counter -= startDelay;
	toggleParticles(false);
	beam = transform.FindChild("Beam");
	scaleBuffer = beam.localScale;
}

function matOffset() {
	beam.renderer.sharedMaterial.mainTextureOffset.x = (beam.renderer.material.mainTextureOffset.x+(animationSpeed*Time.deltaTime*xMultiplier))%1;//Offsets texture for simple flowing animation
	beam.renderer.sharedMaterial.mainTextureOffset.y = (beam.renderer.material.mainTextureOffset.y+(animationSpeed*Time.deltaTime*yMultiplier))%1;//%1 fixates offset between 0 and 1, never above
}

function Update () {
	if(counter > 2)
	matOffset();
	if(beamDuration < 0)
	counter = 2.35;
	else
	counter += Time.deltaTime;
	if(counter < 2 && counter > 1.9){
		beam.localScale.x =beam.localScale.z = scaleBuffer.x*.3;
		beam.gameObject.active = true;
		toggleParticles(true);
		if(audio)
		audio.Play();
		
	}else if(counter < 2.1){
		beam.gameObject.active = false;
		toggleParticles(false);
		
	}else if(counter < 2.2){
		beam.localScale.x =beam.localScale.z = scaleBuffer.x*.7;
		beam.gameObject.active = true;
		toggleParticles(true);
		
	}else if(counter < 2.3){
		beam.gameObject.active = false;
		toggleParticles(false);
	}else if(counter < 2.4){
		beam.localScale.x = beam.localScale.z = scaleBuffer.x;
		beam.gameObject.active = true;
		toggleParticles(true);
		
	}else if(counter > 2.4 + beamDuration){
		beam.gameObject.active = false;
		toggleParticles(false);
		if(audio)
		audio.Stop();
		counter = 0 -pauseDelay +2;
	}
	
}

function toggleParticles(emit:boolean){
	for(var i:int;i<particles.Length;i++){
		if(particles[i])
		particles[i].enableEmission = emit;
	}
}
