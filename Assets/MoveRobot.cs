using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

//having troube making thingd rigid body whiel still letting them move
namespace SubtitleSystem
{
	public class MoveRobot : MonoBehaviour
	{
		public GameObject player;
		public GameObject mainCam;
		float moveSpeed = 2.0f;
		public CharacterController controller;
		public Vector3 moveDirection;
		public Vector3 playerRotation;
		public float playerYAngle;
		public LineRenderer lineRenderer;

		void Start()
		{
			//update subtitle names more often
			//ManagementKeyAttribute subtitles light up or the speaker lighting up when youre facing the speaker
			mainCam = GameObject.Find("Main Camera");
			player = GameObject.Find("player");
			playerYAngle = 0.0f;
			controller = GetComponent<CharacterController>();
			playerRotation = new Vector3(0.0f, 0.0f, 0.0f);
			lineRenderer = this.gameObject.AddComponent<LineRenderer>();
			lineRenderer.startWidth = 0.05F;
			lineRenderer.endWidth = 0.05F;
			//playerDebugLine();
		}

		void Update()
		{
			
			if (Input.GetKey(KeyCode.A))
			{
				transform.Rotate(0.0f, -2.0f, 0.0f, Space.Self);
				playerYAngle += 2.0f;
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.Rotate(0.0f, 2.0f, 0.0f, Space.Self);
				playerYAngle -= 2.0f;
			}
			cameraFollow();
			Move();
			if (GameObject.Find("FirstSubtitleTrigger").GetComponent<AngleSubtitles>().subtitlesTriggered)
			{
				//playerDebugLine();
			}
		}

		private void Move()
		{
			float moveForwardAndBackward = Input.GetAxis("Vertical");
			if (Input.GetKey(KeyCode.W))
			{
				moveDirection = (gameObject.transform.forward * moveSpeed * Time.deltaTime);
				transform.position = new Vector3((moveDirection.x + transform.position.x), 0.5f, (moveDirection.z + transform.position.z));
				cameraFollow();
			}
			/*else if (Input.GetKey(KeyCode.S))
			{
				moveDirection = (gameObject.transform.forward * moveSpeed);
				controller.Move(moveDirection * Time.deltaTime);
			}*/
		}

		//normalize player angles in this scripot as well
		//also check to assign stuff earlier
		void cameraFollow()
		{
			Vector3 newCameraPosition;
			float playerFoward = player.GetComponent<MoveRobot>().playerYAngle;
			Vector3 playerPos = player.GetComponent<Transform>().position;
			float playerZ = playerPos.z;
			float playerX = playerPos.x;
			//why on earth is player angle half of actuall rotation
			float playerAngle = -playerYAngle;
			float camXSin = Mathf.Sin((float)((Math.PI) / 180.0) * (playerAngle));
			float camZCos = Mathf.Cos((float)((Math.PI) / 180.0) * (playerAngle));
			//make sure negetive angles are in the functionality
			float cameraX = playerX - (3.0f) * camXSin;
			float cameraZ = playerZ - (3.0f) * camZCos;
			newCameraPosition = new Vector3(cameraX, 1.3f, cameraZ);
			mainCam.transform.rotation = player.transform.rotation;
			mainCam.GetComponent<Transform>().position = newCameraPosition;
		}

		void playerDebugLine()
		{
			if (lineRenderer != null)
			{
				lineRenderer.positionCount = 2;
				lineRenderer.SetPosition(0, transform.position);
				lineRenderer.SetPosition(1, transform.forward * 20 + transform.position);
			}
		}
	}
}

