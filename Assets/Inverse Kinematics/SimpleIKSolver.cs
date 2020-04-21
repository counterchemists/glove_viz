using UnityEngine;
using System.Collections;

public class SimpleIKSolver : MonoBehaviour
{
    public float IK_POS_THRESH = 0.125f;
	public int MAX_IK_TRIES = 1;

    public float weight = 1.0f;

	[System.Serializable]
	public class JointEntity
	{
		public Transform Joint;
		public AngleRestriction AngleRestrictionRange;

        internal Quaternion _initialRotation;
	}
 
	/// <summary>
	/// Rotation AngleRestriction values in degrees (from -180 to 180).
	/// </summary>
	[System.Serializable]
	public class AngleRestriction
	{
		public bool xAxis = false;
		public float xMin = -180f;
		public float xMax = 180.0f;
		public bool yAxis = false;
		public float yMin = -180f;
		public float yMax = 180f;
		public bool zAxis = false;
		public float zMin = 180f;
		public float zMax = 180f;
	}
 
	public bool IsActive = true;
	public Transform Target;
	public JointEntity[] JointEntities;
	
	public bool IsDamping = false;
	public float DampingMax = 0.5f;
	
	void Start ()
	{
		if (Target == null)
			Target = transform;

		foreach(JointEntity jointEntity in JointEntities) {
			jointEntity._initialRotation = jointEntity.Joint.localRotation;
		}
	}

    private void Update()
    {
        UpdateIK();
    }

    public void UpdateIK ()
	{
		if(IsActive)
			Solve ();
	}
 
	void Solve ()
	{		
		Transform endEffector = JointEntities[JointEntities.Length - 1].Joint;
		Vector3 rootPos, curEnd;
 
		Vector3 targetVector = Vector3.zero;
		Vector3 currentVector = Vector3.zero;
		Vector3 crossResult = Vector3.zero;
 
		float cosAngle,turnAngle;
 
		// START AT THE LAST LINK IN THE CHAIN
		int link = JointEntities.Length - 1;
		int tries = 0;
		
		// SEE IF I AM ALREADY CLOSE ENOUGH
		do {
  
			if (link < 0)
				link = JointEntities.Length - 1;
 
			rootPos = JointEntities[link].Joint.position;
			curEnd = endEffector.position;
 
			// CREATE THE VECTOR TO THE CURRENT EFFECTOR POS
			currentVector = curEnd - rootPos;
			// CREATE THE DESIRED EFFECTOR POSITION VECTOR
			targetVector = Target.position - rootPos;
 
			// NORMALIZE THE VECTORS
			currentVector.Normalize ();
			targetVector.Normalize ();
 
			// THE DOT PRODUCT GIVES ME THE COSINE OF THE DESIRED ANGLE
			cosAngle = Vector3.Dot (currentVector, targetVector);
            Debug.Log(cosAngle);
            // IF THE DOT PRODUCT RETURNS 1.0, I DON'T NEED TO ROTATE AS IT IS 0 DEGREES
            if (cosAngle < 0.99999f) {
				// USE THE CROSS PRODUCT TO CHECK WHICH WAY TO ROTATE
				crossResult = Vector3.Cross (currentVector, targetVector);
				crossResult.Normalize ();
 
				turnAngle = Mathf.Acos (cosAngle); 
				// APPLY DAMPING
				if (IsDamping) {
					if (turnAngle > DampingMax)
						turnAngle = DampingMax;
				}
				turnAngle = turnAngle * Mathf.Rad2Deg;

                Quaternion weightRotation = Quaternion.Lerp(JointEntities[link].Joint.rotation, Quaternion.AngleAxis(turnAngle, crossResult) * JointEntities[link].Joint.rotation, weight);
 
				JointEntities[link].Joint.rotation = weightRotation;
 
				CheckAngleRestrictions (JointEntities[link]);
			}
			link--;
		} while (tries++ < MAX_IK_TRIES && (curEnd - Target.position).sqrMagnitude > IK_POS_THRESH);
	}
 
	/// <summary>
	/// Checks the angle restrictions.
	/// </summary>
	/// <param name="jointEntity">Joint entity</param>
	void CheckAngleRestrictions (JointEntity jointEntity)
	{
		Vector3 euler = jointEntity.Joint.localRotation.eulerAngles;
		
		if(jointEntity.AngleRestrictionRange.xAxis) {
			if(euler.x > 180f)
				euler.x -= 360f;
			euler.x = Mathf.Clamp(euler.x, jointEntity.AngleRestrictionRange.xMin, jointEntity.AngleRestrictionRange.xMax);
		}
		
		if(jointEntity.AngleRestrictionRange.yAxis) {
			if(euler.y > 180f)
				euler.y -= 360f;
			euler.y = Mathf.Clamp(euler.y, jointEntity.AngleRestrictionRange.yMin, jointEntity.AngleRestrictionRange.yMax);
		}
		
		if(jointEntity.AngleRestrictionRange.zAxis) {
			if(euler.z > 180f)
				euler.z -= 360f;
			euler.z = Mathf.Clamp(euler.z, jointEntity.AngleRestrictionRange.zMin, jointEntity.AngleRestrictionRange.zMax);
		}
		
		jointEntity.Joint.localEulerAngles = euler;
	}

	/// <summary>
	/// Reset joints position
	/// </summary>
	public void ResetJoints() {
		foreach(JointEntity jointEntity in JointEntities) {
			jointEntity.Joint.localRotation = jointEntity._initialRotation;
		}
	}
}