#include "stdafx.h"

#include "DetInfWrapper.h"
#include <iostream>

using namespace System;
using namespace System::Collections;

namespace HyWrapper
{
    ////////////////////////////////////////// DetInferenceParameters ////////////////////////////////////////////////////
    DetInferenceParameters::DetInferenceParameters()
    {
        hyParams = nullptr;
    }

    DetInferenceParameters::~DetInferenceParameters()
    {
        SAFE_DELETE(hyParams);
    }

    Hy::Parameters& DetInferenceParameters::ToHyParams()
    {
        SAFE_DELETE(hyParams);
        hyParams = new Hy::DetInferenceParameters();

        msclr::interop::marshal_context context;
        if (detInferenceCfgPath != nullptr)
        {
            hyParams->i_detInferenceCfgPath = context.marshal_as<std::string>(detInferenceCfgPath);
        }

        hyParams->i_inferenceDraw = InferenceDraw;
        hyParams->i_gpuUseful = gpuUseful;
        hyParams->i_engineOptInputSizeH = optInputSizeH;
        hyParams->i_engineOptInputSizeW = optInputSizeW;
        hyParams->i_engineInputHMax = inputMaxH;
        hyParams->i_engineInputWMax = inputMaxW;
        hyParams->i_engineBatch = engineBatch;
        hyParams->i_detMaxDetections = detMaxDetections;
        hyParams->i_detScoreThreshold = detScoreThreshold;

        return *hyParams;
    }

    ////////////////////////////////////////// SegInferenceParameters ////////////////////////////////////////////////////
    DetInferenceImageResults::DetInferenceImageResults()
    {
        hyResult = new Hy::DetInferenceImageResults();
    }

    DetInferenceImageResults::~DetInferenceImageResults()
    {
        SAFE_DELETE(hyResult);
    }

    String^ DetInferenceImageResults::GetName()
    {
        return msclr::interop::marshal_as<String^>(hyResult->GetName());
    }

    void DetInferenceImageResults::FromHyResults(const Hy::Results& rslts)
    {
        const Hy::DetInferenceImageResults& detRslts = dynamic_cast<const Hy::DetInferenceImageResults&>(rslts);
        ImageResults = gcnew array<DetInferenceResultsProperties^>(detRslts.rs_imageResults.size());
        for (int i = 0; i < detRslts.rs_imageResults.size(); i++)
        {
            ImageResults[i] = gcnew DetInferenceResultsProperties();
            ImageResults[i]->clsId = detRslts.rs_imageResults[i].r_clsID;
            ImageResults[i]->clsName = msclr::interop::marshal_as<String^>(detRslts.rs_imageResults[i].r_clsName);
            ImageResults[i]->area = detRslts.rs_imageResults[i].r_area;
            ImageResults[i]->score = detRslts.rs_imageResults[i].r_score;

            if (ImageResults[i]->rectange == nullptr) 
            {
                ImageResults[i]->rectange = (Rect^)RegionFactory::Create(Hy::RegionType::REGION_RECTANGLE);
            }

            ImageResults[i]->rectange->FromHyRect(detRslts.rs_imageResults[i].r_rectange);
        }
    }
}