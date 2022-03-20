using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct RenderPoint {
	static GameObject debugPrefab;
	static RenderPoint() {
		debugPrefab=Resources.Load<GameObject>("Debug");
	}

	public Vector2 point;
	public float timer;
	public float time;
	public bool active;
	GameObject debug;
	public void Update() {
		if(!active) return;
		timer+=Time.deltaTime;
		if(timer>time) {
			active=false;
			Object.Destroy(debug);
		}
	}
	public RenderPoint(Vector2 _point,float _time) {
		point=_point;
		timer=0;
		time=_time;
		active=true;
		debug=Object.Instantiate(debugPrefab,point,Quaternion.identity);
	}
}

[RequireComponent(typeof(Camera))]
public class CameraController:MonoBehaviour {
	public GameObject testMobPrefab;

	void Start() {

		camera=GetComponent<Camera>();
		instance=this;
	}

	void Update() {
		UpdateMousePosition();
		UpdateSelfPosition();
		for(int i = 0;i<100;i++) renderPoints[i].Update();


	}
	private void OnPostRender() {

	}

	//事件

	public class CameraPoiList:List<KeyValuePair<Vector2,float>> { }
	public static event System.EventHandler<CameraPoiList> GetCameraPoi;
	void OnGetPoi(CameraPoiList pois) => GetCameraPoi?.Invoke(this,pois);

	public static CameraController instance;
	public static Vector2 mousePosition { get; private set; }
	static RenderPoint[] renderPoints = new RenderPoint[100];
	public static void DrawPoint(Vector2 point,float time) {
		for(int i = 0;i<100;i++) {
			RenderPoint pointCurrent = renderPoints[i];

			if(pointCurrent.active&&(pointCurrent.point-point).sqrMagnitude<0.001f) {
				pointCurrent.time=Mathf.Max(time,pointCurrent.time-pointCurrent.timer);
				pointCurrent.timer=0;
				return;
			}
		}

		for(int i = 0;i<100;i++) {
			if(!renderPoints[i].active) {
				renderPoints[i]=new RenderPoint(point,time);
				break;
			}
		}
	}

	new public Camera camera { get; private set; }

	void UpdateMousePosition() {
		Vector2 mouseScreenPosition = Input.mousePosition;
		mousePosition=camera.ScreenToWorldPoint(mouseScreenPosition);
	}

	CameraPoiList pois = new CameraPoiList();
	Vector2 selfPosition;
	void UpdateSelfPosition() {

		pois.Clear();
		OnGetPoi(pois);
		float totalWeight = 0;
		Vector2 targetPosition = Vector2.zero;
		foreach(var i in pois) {
			totalWeight+=i.Value;
			targetPosition+=(i.Key*i.Value);
		}
		targetPosition/=totalWeight;

		float speed = (targetPosition-selfPosition).magnitude*5;
		selfPosition=Vector3.MoveTowards(selfPosition,targetPosition,speed*Time.deltaTime);

		transform.position=(Vector3)selfPosition+new Vector3(0,0,transform.position.z);
		//UpdateLookOffset();
		UpdateCameraOffset();
		UpdateScreenshake();
	}

	//由其它物体施加的offset
	public Vector3 targetCameraOffset;//修改这个的值来添加offset
	Vector3 currentCameraOffset;

	const float speedIncreaseThreshold = 4;
	const float targetOffsetSpeedBase = 4;
	const float targetOffsetSpeedIncreaseMultiplier = targetOffsetSpeedBase/speedIncreaseThreshold;
	const float currentOffsetSpeedBase = 6;
	const float currentOffsetSpeedIncreaseMultiplier = currentOffsetSpeedBase/speedIncreaseThreshold;

	void UpdateCameraOffset() {

		float targetOffsetSpeed = targetOffsetSpeedBase;
		if(targetCameraOffset.sqrMagnitude>4) targetOffsetSpeed=targetCameraOffset.sqrMagnitude*targetOffsetSpeedIncreaseMultiplier;
		targetCameraOffset=Vector3.MoveTowards(targetCameraOffset,Vector2.zero,targetOffsetSpeed*Time.deltaTime);


		float currentOffsetSpeed = currentOffsetSpeedBase;
		Vector2 offsetMoveVector = currentCameraOffset-targetCameraOffset;
		if(offsetMoveVector.sqrMagnitude>4) targetOffsetSpeed=offsetMoveVector.sqrMagnitude*currentOffsetSpeedIncreaseMultiplier;
		currentCameraOffset=Vector3.MoveTowards(currentCameraOffset,targetCameraOffset,currentOffsetSpeed*Time.deltaTime);
		transform.position+=currentCameraOffset;
	}

	//屏幕抖动及由屏幕抖动施加的offset
	public void AddScreenShake(float intensity) {
		if(screenShakeIntensityRaw+intensity>intensity*5) screenShakeIntensityRaw=intensity*5;
		else screenShakeIntensityRaw+=Mathf.Abs(intensity);
	}
	float screenShakeIntensityRaw;
	float screenShakeSampleY1;
	float screenShakeSampleY2;
	float screenShakeSampleY3;
	float screenShakeSampleX;
	bool screenShakeResampled;

	void UpdateScreenshake() {
		if(screenShakeIntensityRaw>0) {

			float screenShakeDecreaseSpeed = 5;
			if(screenShakeIntensityRaw>1) screenShakeDecreaseSpeed*=screenShakeIntensityRaw;
			screenShakeIntensityRaw-=screenShakeDecreaseSpeed*Time.deltaTime;

			float screenShakeIntensity = screenShakeIntensityRaw*10;
			if(screenShakeIntensity>1) screenShakeIntensity=Mathf.Sqrt(screenShakeIntensity);
			screenShakeIntensity*=0.1f;

			screenShakeSampleX+=Time.deltaTime*100f;

			//平动
			Vector2 screenShakeVector = new Vector2(
				Mathf.PerlinNoise(screenShakeSampleX,screenShakeSampleY1),
				Mathf.PerlinNoise(screenShakeSampleX,screenShakeSampleY2)
			);

			screenShakeVector-=0.5f*Vector2.one;
			screenShakeVector*=screenShakeIntensity;

			transform.position+=(Vector3)screenShakeVector;

			//转动
			float screenRotate = Mathf.PerlinNoise(screenShakeSampleX,screenShakeSampleY3);

			screenRotate-=0.5f;
			screenRotate*=20;
			screenRotate*=screenShakeIntensity;
			transform.rotation=new Angle(screenRotate).quaternion;

			screenShakeResampled=false;

		} else if(!screenShakeResampled) {
			transform.rotation=Quaternion.identity;
			screenShakeResampled=true;

			screenShakeSampleY1=Random.Range(-4000000f,4000000f);
			screenShakeSampleY2=Random.Range(-4000000f,4000000f);
			screenShakeSampleY3=Random.Range(-4000000f,4000000f);
			screenShakeSampleX=Random.Range(-8000000f,0);

		}

	}


}