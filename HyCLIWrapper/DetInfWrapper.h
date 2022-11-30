#pragma once
#include <msclr/marshal_cppstd.h>
#include <cliext/map>

#include "ViAlgorithmWrapper.h"
#include "../HyAlgorithm/Include/ViAlgorithm/HySmartDetInfDefine.h"

namespace HyWrapper
{
    [Serializable]
    public ref class DetInferenceParameters : public Parameters
    {
    public:
        String^ detInferenceCfgPath = "";
        bool InferenceDraw = false;
        bool gpuUseful = true;
        int optInputSizeH = 320;
        int optInputSizeW = 320;
        int inputMaxH = 640;
        int inputMaxW = 640;
        int engineBatch = 1;
        int detMaxDetections = 10;
        float detScoreThreshold = 0.3f;

    private:
        Hy::DetInferenceParameters* hyParams;

    public:
        DetInferenceParameters();
        ~DetInferenceParameters();

        virtual Hy::Parameters& ToHyParams() override;
    };

    [Serializable]
    public ref class DetInferenceResultsProperties
    {
    public:
        int clsId;
        String^ clsName;
        float area;
        float score;
        Rect^ rectange;
    };

    [Serializable]
    public ref class DetInferenceImageResults : public Results
    {
    public:
        array<DetInferenceResultsProperties^>^ ImageResults;

    private:
        Hy::DetInferenceImageResults* hyResult;

    public:
        DetInferenceImageResults();
        ~DetInferenceImageResults();

        virtual String^ GetName() override;
        virtual void FromHyResults(const Hy::Results& rslts) override;
        virtual Hy::GenericInputs& ToHyInputs() override { return Hy::DetInferenceImageResults(); }
    };
}