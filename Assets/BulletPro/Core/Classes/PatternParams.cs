﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// This script is part of the BulletPro package for Unity.
// Author : Simon Albou <albou.simon@gmail.com>

namespace BulletPro
{
	[System.Serializable]
	public class PatternParams : EmissionParams
	{
		// Used for controlling one pattern from another
		public DynamicString[] patternTags;
		public bool playAtBulletBirth;
		public bool compensateSmallWaits, deltaTimeDisplacement;
		public DynamicFloat defaultInstructionDelay;
		public List<PatternInstructionType> delaylessInstructions;

		// The instruction stack that mimics visual programming.
		// A single pattern can have multiple instruction lists working in parallel.
		// Update from 2020-09: this has been cut, but the code still offers the possibility
		public PatternInstructionList[] instructionLists;
		
		// Is this pattern endless ?
		public bool IsEndless()
		{
			if (instructionLists == null) return false;
			if (instructionLists.Length == 0) return false;
			for (int i = 0; i < instructionLists.Length; i++)
				if (instructionLists[i].IsEndless())
					return true;

			return false;
		}

#if UNITY_EDITOR

		// Simple foldout bool
		public bool advancedFoldout;

		// Allows custom default values
		public bool hasBeenSerializedOnce;

		// Which instruction is being closely inspected ? (for micro-actions)
		public int focusedInstruction = -1;

		// Allows safe edition of the instruction stack during play mode
		public int safetyForPlaymode;

		public override void FirstInitialization()
		{
			hasBeenSerializedOnce = true;

			SetUniqueIndex();

			playAtBulletBirth = true;
			compensateSmallWaits = false;
			deltaTimeDisplacement = false;
			defaultInstructionDelay = new DynamicFloat(0f);
			patternTags = new DynamicString[0];
			instructionLists = new PatternInstructionList[1];
			instructionLists[0].FirstInitialization();

			delaylessInstructions = new List<PatternInstructionType>();
			delaylessInstructions.Add(PatternInstructionType.Wait);
			delaylessInstructions.Add(PatternInstructionType.BeginLoop);
			delaylessInstructions.Add(PatternInstructionType.EndLoop);
			delaylessInstructions.Add(PatternInstructionType.SetInstructionDelay);
			delaylessInstructions.Add(PatternInstructionType.Die);
		}
#endif
	}

	// An enum that stores every possible instruction given to a Pattern behaviour
	[System.Serializable]
	public enum PatternInstructionType
	{
		// Main
		Wait,
		Shoot,
		BeginLoop,
		EndLoop,
		PlayVFX,
		StopVFX,
		PlayAudio,
		Die,

		// Transform - misc
		EnableMovement,
		DisableMovement,
		AttachToEmitter,
		DetachFromEmitter,

		// Transform - position
		TranslateGlobal,
		TranslateLocal,
		SetWorldPosition,
		SetLocalPosition,
		SetSpeed,
		MultiplySpeed,

		// Transform - rotation
		Rotate,
		SetWorldRotation,
		SetLocalRotation,
		SetAngularSpeed,
		MultiplyAngularSpeed,

		// Transform - scale
		SetScale,
		MultiplyScale,

		// Homing
		EnableHoming,
		DisableHoming,
		TurnToTarget,
		ChangeTarget,
		SetHomingSpeed,
		MultiplyHomingSpeed,
		ChangeHomingTag,

		// Curves - simple controls
		EnableCurve,
		DisableCurve,
		PlayCurve,
		PauseCurve,
		RewindCurve,
		ResetCurve,
		StopCurve,
		// Curves - change values
		SetCurveValue,
		SetWrapMode,
		SetPeriod,
		MultiplyPeriod,
		SetCurveRawTime,
		SetCurveRatio,

		// Graphics
		TurnVisible,
		TurnInvisible,
		PlayAnimation,
		PauseAnimation,
		RebootAnimation,

		// Graphics/Color
		SetColor,
		AddColor,
		MultiplyColor,
		OverlayColor,
		SetAlpha,
		AddAlpha,
		MultiplyAlpha,
		SetLifetimeGradient,

		// Collision
		EnableCollision,
		DisableCollision,
		ChangeCollisionTag,

		// Pattern Flow
		PlayPattern,
		PausePattern,
		StopPattern,
		RebootPattern,
		SetInstructionDelay,

		// Random Seed
		FreezeRandomSeed,
		UnfreezeRandomSeed,
		RerollRandomSeed,
		SetRandomSeed,

		// Custom Parameters - numbers
		SetCustomInteger,
		AddCustomInteger,
		MultiplyCustomInteger,
		SetCustomFloat,
		AddCustomFloat,
		MultiplyCustomFloat,
		SetCustomSlider01,
		AddCustomSlider01,
		MultiplyCustomSlider01,
		SetCustomDouble,
		AddCustomDouble,
		MultiplyCustomDouble,
		SetCustomLong,
		AddCustomLong,
		MultiplyCustomLong,

		// Custom Parameters - vectors and colors
		SetCustomVector2,
		AddCustomVector2,
		MultiplyCustomVector2,
		SetCustomVector3,
		AddCustomVector3,
		MultiplyCustomVector3,
		SetCustomVector4,
		AddCustomVector4,
		MultiplyCustomVector4,
		SetCustomColor,
		AddCustomColor,
		MultiplyCustomColor,
		OverlayCustomColor,

		// Custom Parameters - the rest
		SetCustomGradient,
		SetCustomBool,
		SetCustomString,
		AppendToCustomString,
		SetCustomAnimationCurve,
		SetCustomObject,
		SetCustomQuaternion,
		SetCustomRect,
		SetCustomBounds
	}

	// enums used as arguments in an instruction
	[System.Serializable]
	public enum CollisionTagAction { Add, Remove }
	[System.Serializable]
	public enum PatternControlTarget { ThisPattern, AnotherPattern }
	[System.Serializable]
	public enum InstructionTiming { Instantly, Progressively }
	[System.Serializable]
	public enum CurvePeriodType { FixedValue, BulletTotalLifetime, RemainingLifetime }
	[System.Serializable]
	public enum VFXPlayType { Default, Custom }
	[System.Serializable]
	public enum VFXFilterType { Index, Tag }

	// The foundations of a pattern behaviour. Users have access to visual programming using these.
	[System.Serializable]
	public struct PatternInstruction
	{
		public bool enabled;
		public PatternInstructionType instructionType;

		// wait
		public DynamicFloat waitTime;
		// shoot
		public DynamicShot shot;
		// loops
		public bool endless;
		public DynamicInt iterations;
		// also used for "set global/local position"
		public DynamicVector2 globalMovement, localMovement;
		// used for rotate and set world/local rotation
		public DynamicFloat rotation;
		// used for setting speed, angSpeed, homSpeed
		public DynamicFloat speedValue;
		// used for setting scale
		public DynamicFloat scaleValue;
		// used for all the "multiply something" instructions
		public DynamicFloat factor;
		// launching effects
		public VFXPlayType vfxPlayType; // deprecated, but used for updating EmitterProfiles to 1.2
		public VFXFilterType vfxFilterType;
		public DynamicObjectReference audioClip;
		public DynamicObjectReference vfxToPlay; // deprecated, but used for updating EmitterProfiles to 1.2
		public DynamicInt vfxIndex;
		public DynamicString vfxTag;
		// turn to target
		public DynamicFloat turnIntensity;
		// homing
		public DynamicEnum preferredTarget;
		// collision tags
		public CollisionTagAction collisionTagAction;
		public DynamicString collisionTag;
		// control patterns
		public PatternControlTarget patternControlTarget;
		public DynamicString patternTag;
		public DynamicSlider01 newRandomSeed;
		// curves
		public DynamicAnimationCurve newCurveValue;
		public DynamicFloat newPeriodValue;
		public CurvePeriodType newPeriodType;
		public DynamicEnum newWrapMode;
		public DynamicFloat curveRawTime;
		public DynamicSlider01 curveTime;
		// Graphics
		public DynamicColor color;
		public DynamicSlider01 alpha; // for set and add
		public DynamicGradient gradient;

		// general
		public PatternCurveType curveAffected;
		public bool canBeDoneOverTime; // can be a micro-action
		public InstructionTiming instructionTiming;
		public DynamicFloat instructionDuration;
		public DynamicAnimationCurve operationCurve;

		// custom params
		public string customParamName;
		public DynamicInt customInt;
		public DynamicFloat customFloat;
		public DynamicSlider01 customSlider01;
		public double customDouble;
		public long customLong;
		public DynamicVector2 customVector2;
		public DynamicVector3 customVector3;
		public DynamicVector4 customVector4;
		public DynamicColor customColor;
		public DynamicGradient customGradient;
		public DynamicBool customBool;
		public DynamicString customString;
		public DynamicAnimationCurve customAnimationCurve;
		public DynamicObjectReference customObjectReference;
		public Quaternion customQuaternion;
		public DynamicRect customRect;
		public Bounds customBounds;

		#if UNITY_EDITOR
		public string displayName;
		#endif
	}

	// The instruction stack that mimics visual programming.
	[System.Serializable]
	public struct PatternInstructionList
	{
		public PatternInstruction this[int i] { get { return instructions[i]; } }
		public PatternInstruction[] instructions;

		// Is this routine endless ?
		public bool IsEndless()
		{
			if (instructions == null) return false;
			if (instructions.Length == 0) return false;
			for (int i = 0; i < instructions.Length; i++)
			{
				// TODO : detect edge-situations where it reboots patterns with a tag, and bears this same tag (thus auto-rebooting)
				if (instructions[i].instructionType == PatternInstructionType.RebootPattern
					&& instructions[i].patternControlTarget == PatternControlTarget.ThisPattern)
					return true;

				if (instructions[i].instructionType == PatternInstructionType.BeginLoop)
					if (instructions[i].endless)
						return true;
			}

			return false;
		}

		#if UNITY_EDITOR
		public void FirstInitialization()
		{
			instructions = new PatternInstruction[5];

			for (int i = 0; i < instructions.Length; i++)
			{
				instructions[i].endless = true;
				instructions[i].waitTime = new DynamicFloat(1f);
				instructions[i].iterations = new DynamicInt(1);
				instructions[i].enabled = true;
				instructions[i].instructionType = PatternInstructionType.Wait;

				instructions[i].localMovement = new DynamicVector2(Vector2.zero);
				instructions[i].globalMovement = new DynamicVector2(Vector2.zero);
				instructions[i].rotation = new DynamicFloat(0f);
				instructions[i].speedValue = new DynamicFloat(0f);
				instructions[i].scaleValue = new DynamicFloat(1f);
				instructions[i].factor = new DynamicFloat(1f);
				
				instructions[i].audioClip = new DynamicObjectReference(null);
				instructions[i].audioClip.SetNarrowType(typeof(AudioClip));
				instructions[i].vfxToPlay = new DynamicObjectReference(null);
				instructions[i].vfxToPlay.SetNarrowType(typeof(ParticleSystem));
				instructions[i].vfxFilterType = VFXFilterType.Index;
				instructions[i].vfxIndex = new DynamicInt(0);
				instructions[i].vfxTag = new DynamicString("");

				instructions[i].shot = new DynamicShot(null);

				instructions[i].preferredTarget = new DynamicEnum(0);
				instructions[i].preferredTarget.SetEnumType(typeof(PreferredTarget));
				instructions[i].turnIntensity = new DynamicFloat(1f);
				instructions[i].turnIntensity.EnableSlider(-1f, 1f);
				instructions[i].collisionTagAction = CollisionTagAction.Add;
				instructions[i].collisionTag = new DynamicString("Player");
				instructions[i].patternTag = new DynamicString("");
				instructions[i].patternControlTarget = PatternControlTarget.ThisPattern;
				instructions[i].newRandomSeed = new DynamicSlider01(0f);
				
				instructions[i].newCurveValue = new DynamicAnimationCurve(AnimationCurve.Constant(0,1,1));
				instructions[i].newCurveValue.SetForceZeroToOne(true);
				instructions[i].newPeriodValue = new DynamicFloat(1f);
				instructions[i].newWrapMode = new DynamicEnum(0);
				instructions[i].newWrapMode.SetEnumType(typeof(WrapMode));
				instructions[i].curveRawTime = new DynamicFloat(0f);
				instructions[i].curveTime = new DynamicSlider01(0f);

				instructions[i].color = new DynamicColor(Color.black);
				instructions[i].alpha = new DynamicSlider01(1f);
				Gradient grad = new Gradient();
				GradientAlphaKey[] gak = new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) };
				GradientColorKey[] gck = new GradientColorKey[] { new GradientColorKey(Color.black, 0), new GradientColorKey(Color.black, 1) };
				grad.SetKeys(gck, gak);
				instructions[i].gradient = new DynamicGradient(grad);

				instructions[i].instructionTiming = InstructionTiming.Instantly;
				instructions[i].instructionDuration = new DynamicFloat(1f);
				instructions[i].operationCurve = new DynamicAnimationCurve(AnimationCurve.EaseInOut(0, 0, 1, 1));
				instructions[i].operationCurve.SetForceZeroToOne(true);

				instructions[i].displayName = "Wait";
				instructions[i].canBeDoneOverTime = false;

				instructions[i].customParamName = "_PowerLevel";
				instructions[i].customInt = new DynamicInt(0);
				instructions[i].customFloat = new DynamicFloat(0f);
				instructions[i].customSlider01 = new DynamicSlider01(0f);
				instructions[i].customVector2 = new DynamicVector2(Vector2.zero);
				instructions[i].customVector3 = new DynamicVector3(Vector3.zero);
				instructions[i].customVector4 = new DynamicVector4(Vector4.zero);
				instructions[i].customColor = new DynamicColor(Color.black);
				instructions[i].customGradient = new DynamicGradient(grad);
				instructions[i].customBool = new DynamicBool(false);
				instructions[i].customString = new DynamicString("");
				instructions[i].customAnimationCurve = new DynamicAnimationCurve(AnimationCurve.EaseInOut(0, 0, 1, 1));
				instructions[i].customObjectReference = new DynamicObjectReference(null);
				instructions[i].customRect = new DynamicRect(Rect.zero);
			}

			instructions[0].instructionType = PatternInstructionType.BeginLoop;
			instructions[0].displayName = "Begin Loop";
			instructions[1].instructionType = PatternInstructionType.Shoot;
			instructions[1].displayName = "Shoot";
			instructions[2].instructionType = PatternInstructionType.PlayAudio;
			instructions[2].displayName = "Play Audio";
			instructions[3].instructionType = PatternInstructionType.Wait;
			instructions[3].displayName = "Wait";
			instructions[4].instructionType = PatternInstructionType.EndLoop;
			instructions[4].displayName = "End Loop";
		}
		#endif
	}

	[System.Serializable]
	public enum PatternCurveType { Speed, AngularSpeed, Scale, Homing, Color, Alpha, AnimX, AnimY, AnimAngle, AnimScale, None }
}
