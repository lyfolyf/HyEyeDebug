#pragma once
#include <msclr/marshal_cppstd.h>
#include <cliext/map>

#include "ViAlgorithmWrapper.h"
#include "../HyAlgorithm/Include/ViAlgorithm/HySmartSegInfDefine.h"

namespace HyWrapper
{
    [Serializable]
    public ref class SegInferenceParameters : public Parameters
    {
    public:
        String^ segInferenceCfgPath = "";
        bool gpuUseful = true;
        bool isPatch = false;
        bool inferenceDraw = false;
        int padding = 0;
        int batchMax = 10;
        int optBatch = 5;
        int batchPatchSplit = 2;
        int batchSplitThreshold = 10;

    private:
        Hy::SegInferenceParameters* hyParams;

    public:
        SegInferenceParameters();
        ~SegInferenceParameters();

        virtual Hy::Parameters& ToHyParams() override;
    };

    [Serializable]
    public ref class SegInferenceResultsProperties
    {
    public:
        int clsId;
        String^ clsName;
        float area;
        Rect^ rectange;
        Point2f^ centroids;
        Contour^ contour;
    };

    [Serializable]
    public ref class SegInferenceImageResults : public Results
    {
    public:
        array<SegInferenceResultsProperties^>^ ImageResults;

    private:
        Hy::SegInferenceImageResults* hyResult;

    public:
        SegInferenceImageResults();
        ~SegInferenceImageResults();

        virtual String^ GetName() override;
        virtual void FromHyResults(const Hy::Results& rslts) override;
        virtual Hy::GenericInputs& ToHyInputs() override { return Hy::SegInferenceImageResults(); }
    };
}
