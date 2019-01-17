// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Custom Shader"
{
	Properties
	{
		_Tint ("Tint", Color) = (1,1,1,1)
	}

    SubShader //Sub Shader consists of the shader information. Can have 1 subshader for one thing and another for another thing
	{
		Pass //Function that does the necessary Renders. A Shader can have more than 1 Pass
		{
			CGPROGRAM
			 //Tell Compiler which program to use
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc" //Contains Generic Functions for Unity Shaders
			/*#include "UnityShaderVariables.cginc" //Defines a whole bunch of shader variables that are necessary for rendering, like transformation, camera, and light data. These are all set by Unity when needed.
			#include "HLSLSupport.cginc" //Sets things up so you can use the same code no matter which platform you're targeting. So you don't need to worry about using platform-specific data types and such.
			#include "UnityInstancing.cginc"*/ //Specifically for instancing support, which is a specific rendering technique to reduce draw calls. Although it doesn't include the file directly, it depends on UnityShaderVariables.

			float4 _Tint; //Add Property as Variable in order to use it

			//Pragmas have to be specified like that and they work something like functions
			//SEMANTICS are needed as they are like additional data types that is required for the Shader to know what to do with it
			float4 MyVertexProgram(float4 position : POSITION):SV_POSITION //Reason why float4 is because Unity requires 4x4 transformation matrix. SV stands for System Value and POSITION refers to final vertex position 
			//Hence output will contain the final transformed vertex position used for rasterization
			{
				return UnityObjectToClipPos(position);	//Multiply so that it will be correctly displayed
			}

			//Local 
			float4 MyFragmentProgram(float4 postion : SV_POSITION, float3 localPosition : TEXCOORD0):SV_TARGET
			{
				return float4 (localPosition, 1); //_Tint;
			}

			ENDCG
		}
	}
}
