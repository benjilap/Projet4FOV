using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SecurityCam : MonoBehaviour {


    [SerializeField]
    public float angleInit;

    [SerializeField]
    float angleRotation;

    Quaternion _angleInit;


    public float timeTraveling = 3f;
    public float timeWaiting = 1f;

    public AnimationCurve curve;

    FieldOfViewSecurity ownFow;
    public float detectionTimer = 2.0f;
    public float detectionSpeed = 1.0f;
    private bool playerInFow;
    private bool startTimer;
    private float timerReset;
    private float DstToPlayer;
    public float timer;
    [HideInInspector]
    public bool playerSpotted;

    Color normalStateColor = new Vector4(0, 0, 1, 0.5f);
    Color warningStateColor = new Vector4(1, 0.5f, 0, 0.5f);
    Color alertStateColor = new Vector4(1, 0, 0, 0.5f);

    [SerializeField]
    Shader stencilObject;
    public Material modelMaterial;
    Material viewMaterial;
    Renderer visionMesh;

    GameObject warningPoint;
    GameObject alertPoint;

    PatrolAction guardMove;

    void Start()
    {
        ownFow = GetComponent<FieldOfViewSecurity>();
        _angleInit = Quaternion.Euler(0, angleInit, 0);
        transform.rotation = _angleInit;

        startTimer = true;

        StartCoroutine(cr_Patrol());

        viewMaterial = new Material(stencilObject);
        viewMaterial.mainTexture = modelMaterial.mainTexture;
        viewMaterial.color = new Vector4(0, 0, 1, 0.5f);
        visionMesh = this.transform.GetChild(0).GetComponent<Renderer>();

        if (this.transform.parent.GetComponent<PatrolAction>() == true)
        {
            guardMove = this.transform.parent.GetComponent<PatrolAction>();
        }

        warningPoint = this.transform.parent.GetChild(1).GetChild(0).GetChild(1).gameObject;
        alertPoint = this.transform.parent.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }
    
    void Update()
    {
        visionMesh.material = viewMaterial;
        if (ownFow.visibleTargets.Count == 0)
        {
            Detection(null);
        }
        else
        {
            foreach (Transform playerTarget in ownFow.visibleTargets)
            {
                Detection(playerTarget);
            }
        }

        if (angleRotation == 0 && guardMove != null)
        {
            FowDir();
        }
    }


    void Detection(Transform player)
    {

        if (player != null)
        {
            playerInFow = true;
            DstToPlayer = Vector3.Distance(transform.position, player.position);



            if (playerInFow == true && startTimer == true)
            {
                timerReset = detectionTimer * DstToPlayer;
                timer = timerReset;
                startTimer = false;
            }

            if (guardMove != null)
            {
                Debug.Log(guardMove);
                guardMove._waiting = true;
                guardMove._waitTimer = 0f;
                guardMove._totalWaitTime = timerReset;
                guardMove._navMeshAgent.isStopped = true;
            }


            if (startTimer == false)
            {
                timer -= Time.deltaTime * detectionSpeed;
                if (timer > timerReset / 2)
                {
                    viewMaterial.color = Color.Lerp(normalStateColor, warningStateColor, (timerReset - timer));
                    warningPoint.SetActive(true);
                }
                else
                {
                    warningPoint.SetActive(false);
                    alertPoint.SetActive(true);
                    viewMaterial.color = Color.Lerp(warningStateColor, alertStateColor, (timerReset - timer) - (timerReset / 2));
                }
                if (timer <= 0)
                {
                    playerSpotted = true;
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
        else if (player == null)
        {
            viewMaterial.color = normalStateColor;
            playerInFow = false;
            startTimer = true;

            warningPoint.SetActive(false);
            alertPoint.SetActive(false);
            if (guardMove != null)
            {
                guardMove._navMeshAgent.isStopped = false;
            }
        }
    }

    void FowDir()
    {
        if (guardMove._navMeshAgent.velocity.normalized.x < -0.5f)
        {
            if (guardMove._navMeshAgent.velocity.normalized.z < 0.5f)
            {
                if (guardMove._navMeshAgent.velocity.normalized.z > -0.5f)
                {
                    this.transform.localEulerAngles = new Vector3(0, 270, 0) - this.transform.parent.localEulerAngles;
                }
            }
        }
        else

        if (guardMove._navMeshAgent.velocity.normalized.x > 0.5f)
        {
            if (guardMove._navMeshAgent.velocity.normalized.z < 0.5f)
            {
                if (guardMove._navMeshAgent.velocity.normalized.z > -0.5f)
                {
                    this.transform.localEulerAngles = new Vector3(0, 90, 0) - this.transform.parent.localEulerAngles;
                }
            }
        }
        else

        if (guardMove._navMeshAgent.velocity.normalized.z < -0.5f)
        {
            if (guardMove._navMeshAgent.velocity.normalized.x < 0.5f)
            {
                if (guardMove._navMeshAgent.velocity.normalized.x > -0.5f)
                {
                    this.transform.localEulerAngles = new Vector3(0, 180, 0) - this.transform.parent.localEulerAngles;
                }
            }
        }
        else
        if (guardMove._navMeshAgent.velocity.normalized.z > 0.5f)
        {
            if (guardMove._navMeshAgent.velocity.normalized.x < 0.5f)
            {
                if (guardMove._navMeshAgent.velocity.normalized.x > -0.5f)
                {
                    this.transform.localEulerAngles = new Vector3(0, 0, 0) - this.transform.parent.localEulerAngles;
                }
            }
        }

    }

    IEnumerator cr_Patrol()
    {
        if (playerInFow == false)
        {
            while (true)
            {

                //Travelling 1
                float startTime = Time.time;
                Vector3 initAngles = this.transform.localEulerAngles;
                while (Time.time - startTime <= timeTraveling)
                {
                    float t = (Time.time - startTime) / timeTraveling;
                    this.transform.localEulerAngles = initAngles + curve.Evaluate(t) * angleRotation * Vector3.up;
                    yield return 0;

                }

                //Wait1
                startTime = Time.time;
                while (Time.time - startTime <= timeWaiting)
                {
                    yield return 0;
                }

                //Travelling 2
                startTime = Time.time;
                initAngles = this.transform.localEulerAngles;
                while (Time.time - startTime <= timeTraveling)
                {
                    float t = (Time.time - startTime) / timeTraveling;
                    this.transform.localEulerAngles = initAngles - curve.Evaluate(t) * angleRotation * Vector3.up;
                    yield return 0;

                }

                //Wait2
                startTime = Time.time;
                while (Time.time - startTime <= timeWaiting)
                {
                    yield return 0;
                }
            }
        }
    }
}
