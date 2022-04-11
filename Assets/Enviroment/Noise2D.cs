using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise2D {

	public float frequency;

	float sampleY;
	float sampleX1;
	float sampleX2;

	bool lastSample=true;

	public Noise2D(float frequency){
		this.frequency=frequency;
	}

	public Vector2 Sample(float magnitude) {
		if(magnitude==0) {
			if(lastSample) {
				sampleX1=Random.Range(-100000,100000);
				sampleX2=Random.Range(-100000,100000);
				sampleY=Time.time+Random.Range(-100000,100000);
			}
			lastSample=false;
			return Vector2.zero;
		} else {
			lastSample=true;
			return new Vector2(SampleFloat(sampleX1,magnitude),SampleFloat(sampleX2,magnitude));
		}
	}

	float SampleFloat(float sampleX,float magnitude) {
		float y = (Time.time-sampleY)*frequency;
		float result = Mathf.PerlinNoise(sampleX,y);
		result-=0.5f;
		result*=2*magnitude;
		return result;
	}

}
