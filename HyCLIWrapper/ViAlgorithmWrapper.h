#pragma once
#include <msclr/marshal_cppstd.h>
#include <cliext/map>

#include "HyTypesWrapper.h"
#include "../HyAlgorithm/Include/ViAlgorithm/ViAlgorithmProcess.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Drawing;
using namespace System::Collections;

namespace HyWrapper
{
#define SAFE_DELETE(p) { if ((p) != nullptr) {delete (p); (p) = nullptr;} }

    public enum class AlgorithmType
    {
        ALGORITHM_UNKNOWN = Hy::ALGORITHM_UNKNOWN,
        ALGORITHM_CROP_IMAGE = Hy::ALGORITHM_CROP_IMAGE,
        ALGORITHM_AFFINE_TRANS = Hy::ALGORITHM_AFFINE_TRANS,
        ALGORITHM_CALCUAFFINE_TRANS = Hy::ALGORITHM_CALCUAFFINE_TRANS,
        ALGORITHM_BREAK_CONTOUR = Hy::ALGORITHM_BREAK_CONTOUR,
        ALGORITHM_FIND_MODEL = Hy::ALGORITHM_FIND_MODEL,
        ALGORITHM_CONVOL_IMAGE = Hy::ALGORITHM_CONVOL_IMAGE,
        ALGORITHM_CURVE2LINE_IMAGE = Hy::ALGORITHM_CURVE2LINE_IMAGE,
        ALGORITHM_EDGEREFLECT_IMAGE = Hy::ALGORITHM_EDGEREFLECT_IMAGE,
        ALGORITHM_GETGRAY_IMAGE = Hy::ALGORITHM_GETGRAY_IMAGE,
        ALGORITHM_GRADIENT_IMAGE = Hy::ALGORITHM_GRADIENT_IMAGE,
        ALGORITHM_ENHANCEMENT_IMAGE = Hy::ALGORITHM_ENHANCEMENT_IMAGE,
        ALGORITHM_FFTADJUST_IMAGE = Hy::ALGORITHM_FFTADJUST_IMAGE,
        ALGORITHM_FILTER_IMAGE = Hy::ALGORITHM_FILTER_IMAGE,
        ALGORITHM_OPERATION_IMAGE = Hy::ALGORITHM_OPERATION_IMAGE,
        ALGORITHM_RESIZE_IMAGE = Hy::ALGORITHM_RESIZE_IMAGE,
        ALGORITHM_TRANSFORM_IMAGE = Hy::ALGORITHM_TRANSFORM_IMAGE,
        ALGORITHM_MORPHOLOGY_IMAGE = Hy::ALGORITHM_MORPHOLOGY_IMAGE,
        ALGORITHM_POLARTRANS_IMAGE = Hy::ALGORITHM_POLARTRANS_IMAGE,
        ALGORITHM_RESTORE2CURVE_IMAGE = Hy::ALGORITHM_RESTORE2CURVE_IMAGE,
        ALGORITHM_SCALERANGE_IMAGE = Hy::ALGORITHM_SCALERANGE_IMAGE,
        ALGORITHM_HOMOFILTER_IMAGE = Hy::ALGORITHM_HOMOFILTER_IMAGE,

        ALGORITHM_SEGINFERENCE = Hy::ALGORITHM_SEGINFERENCE,
        ALGORITHM_DETINFERENCE = Hy::ALGORITHM_DETINFERENCE
    };

    public enum class ReferenceAlgorithmFunction
    {
        REF_ALGORITHM_FUNCTION_NONE = Hy::REF_ALGORITHM_FUNCTION_NONE,
        REF_ALGORITHM_POSITIONING = Hy::REF_ALGORITHM_POSITIONING,
        REF_ALGORITHM_REGION_MASKING = Hy::REF_ALGORITHM_REGION_MASKING,
        REF_ALGORITHM_PREPROCESSING = Hy::REF_ALGORITHM_PREPROCESSING,
        REF_ALGORITHM_EMPTY_PICKING_CHECK = Hy::REF_ALGORITHM_EMPTY_PICKING_CHECK
    };

    public enum class TransformType
    {
        TRANSFORM_NONE = Hy::TRANSFORM_NONE,
        TRANSFORM_REGION = Hy::TRANSFORM_REGION,
        TRANSFORM_IMAGE = Hy::TRANSFORM_IMAGE
    };

    public enum class ResultJudgeType
    {
        RSLT_NONE = Hy::RSLT_NONE,
        RSLT_OK = Hy::RSLT_OK,
        RSLT_NOT_ACCEPTABLE = Hy::RSLT_NOT_ACCEPTABLE,
        RSLT_NG = Hy::RSLT_NG,
        RSLT_INVALID = Hy::RSLT_INVALID
    };

    [Serializable]
    public ref class ReferenceAlgorithm
    {
    public:
        String^ preLinkAlgorithmId;  // 前面算子ID
        String^ preLinkOutParamName; // 前面算子参数名
        String^ inParamName;  // 本身算子参数名

        ReferenceAlgorithm() : preLinkAlgorithmId(""), preLinkOutParamName(""), inParamName("") {}
        ReferenceAlgorithm(String^ preLinkAlgorithmId, String^ preLinkOutParamName, String^ inParamName)
        {
            this->preLinkAlgorithmId = preLinkAlgorithmId;
            this->preLinkOutParamName = preLinkOutParamName;
            this->inParamName = inParamName;
        }
    };

    [Serializable]
    public ref class AlgorithmProperties
    {
    public:
        String^ id;
        String^ alias;
        String^ mode;
        String^ predecessorId;   // 前ID ,执行顺序
        String^ successorId;      // 后ID ,执行顺序

        array<ReferenceAlgorithm^>^ referenceAlgorithmList;

        AlgorithmProperties() {}
        AlgorithmProperties(String^ id, String^ alias, String^ mode, String^ predecessorId, String^ successorId, array<ReferenceAlgorithm^>^ referenceAlgorithmList)
        {
            this->id = id;
            this->alias = alias;
            this->mode = mode;
            this->predecessorId = predecessorId;
            this->successorId = successorId;
            this->referenceAlgorithmList = referenceAlgorithmList;
        }
    };

    //@FIXME: is there a better way (automatic) to convert Unmanaged C++ to C++/CLI?
    [Serializable]
    public ref class Parameters abstract
    {
    public:
        virtual Hy::Parameters& ToHyParams() abstract;
    };

    [Serializable]
    public ref class GenericInputs abstract
    {
    public:
        virtual Hy::GenericInputs& ToHyInputs() abstract;

    public:
        Image^ inputImg;
        Image^ maskImg;
        Image^ preprocessedImg;
        AlgorithmProperties^ Properties;
        String^ id;
        UINT64 frameIndex; // @TODO: Later this frameIndex need to be assigned by c++ algorithem layer.
    };

    [Serializable]
    public ref class Results abstract : public GenericInputs
    {
    public:
        virtual void FromHyResults(const Hy::Results& rslts) abstract;
        virtual void ScalingResults(const float scaling) {}
        virtual String^ GetName() abstract;
        virtual ~Results() {};

    public:
        ResultJudgeType judgeType;
        ResultJudgeType outputJudge; // This is the final judge of a vision tool link.
        array<String^>^ ngOutputItems;
    };


    //////////////////////////////////////////////////////////////////////////////////////////////
    // Vision Algorithm
    //////////////////////////////////////////////////////////////////////////////////////////////
    public ref class ViAlgorithm
    {
    private:
        Hy::IViAlgorithm* m_hyAlgorithm;
        Hy::AlgorithmProperties* m_hyProperties;
        array<Results^>^ m_outputArr;
        Results^ m_output;
        std::vector<const Hy::GenericInputs*>* m_hyInputs;

    public:
        ViAlgorithm();
        explicit ViAlgorithm(int algorithmType);
        virtual ~ViAlgorithm();
        virtual int Initialize();
        virtual int GetType();
        virtual String^ GetName();
        virtual void SetProperties(AlgorithmProperties^ properties);
        virtual AlgorithmProperties^ GetProperties();
        virtual int SetParameters(Parameters^ params);
        virtual Hy::IViAlgorithm* GetHyAlgorithm();
        virtual int Process(Image^ inputImg, Rect^ inputRect, [Out] array<Results^>^% outputs);
        virtual int Process(Image^ inputImg, [Out] array<Results^>^% outputs);
        virtual int Process(array<Image^>^ inputImgs, [Out] array<Results^>^% outputs);
        //@XXX: inputs/outputs conversion is redundant, for vision tool chain processing we need to pass down the linked vision tools
        // directly (by using VisionToolChain class) and process them at the lower level.
        virtual int Process(array<GenericInputs^>^ inputs, [Out] array<Results^>^% outputs);
        virtual int GetResults([Out] array<Results^>^% output);

    protected:
        void HyResultsToResults(std::vector<const Hy::Results*>& mvOutput);
    };

    ///////////////////////////////////////////////Vision Engine Chain///////////////////////////////////////////
    [Serializable]
    public ref class ViAlgorithmChain
    {
    public:
        ViAlgorithmChain();
        virtual ~ViAlgorithmChain();
        int Process(Image^ inputImg, array<ViAlgorithm^>^ algorithms, [Out] array<Results^>^% outputs);

    private:
        void HyResultsToResults(std::vector<const Hy::Results*>& hyOutput);

    private:
        array<Results^>^ m_outputArr;
        Results^ m_output;
        std::vector<const Hy::GenericInputs*>* m_hyInputs;
    };
}
