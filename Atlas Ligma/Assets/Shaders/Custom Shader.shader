// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Custom Shader"
{
	Properties
	{
		_Tint ("Tint", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
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
			sampler2D _MainTex; 

			struct Interpolators
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			struct VertexData
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			}

			//Pragmas have to be specified like that and they work something like functions
			//SEMANTICS are needed as they are like additional data types that is required for the Shader to know what to do with it
			Interpolators MyVertexProgram(VertexData v) //Reason why float4 is because Unity requires 4x4 transformation matrix. SV stands for System Value and POSITION refers to final vertex position 
			//Hence output will contain the final transformed vertex position used for rasterization
			{
				Interpolators i;
				i.position = UnityObjectToClipPos(v.position);	//Multiply so that it will be correctly displayed
				i.uv = v.uv;
				return i;
			}

			//Local 
			float4 MyFragmentProgram(Interpolators i):SV_TARGET
			{
				return tex2D (_MainTex, i.uv); //* _Tint;
			}

			ENDCG
		}
	}
}
