#include "stdafx.h"

#include "SegInfWrapper.h"
#include <iostream>

using namespace System;
using namespace System::Collections;

namespace HyWrapper
{
    ////////////////////////////////////////// DetInferenceParameters ////////////////////////////////////////////////////
    SegInferenceParameters::SegInferenceParameters()
    {
        hyParams = nullptr;
    }

    SegInferenceParameters::~SegInferenceParameters()
    {
        SAFE_DELETE(hyParams);
    }

    Hy::Parameters& SegInferenceParameters::ToHyParams()
    {
        SAFE_DELETE(hyParams);
        hyParams = new Hy::SegInferenceParameters();

        msclr::interop::marshal_context context;
        if (segInferenceCfgPath != nullptr)
        {
            hyParams->i_segInferenceCfgPath = context.marshal_as<std::string>(segInferenceCfgPath);
        }

        hyParams->i_gpuUseful = gpuUseful;
        hyParams->i_isPatch = isPatch;
        hyParams->i_padding = padding;
        hyParams->i_inferenceDraw = inferenceDraw;
        hyParams->i_batchMax = batchMax;
        hyParams->i_optBatch = optBatch;
        hyParams->i_batchPatchSplit = batchPatchSplit;
        hyParams->i_batchSplitThreshold = batchSplitThreshold;

        return *hyParams;
    }

    ////////////////////////////////////////// SegInferenceParameters ////////////////////////////////////////////////////
    SegInferenceImageResults::SegInferenceImageResults()
    {
        hyResult = new Hy::SegInferenceImageResults();
    }

    SegInferenceImageResults::~SegInferenceImageResults()
    {
        SAFE_DELETE(hyResult);
    }

    String^ SegInferenceImageResults::GetName()
    {
        return msclr::interop::marshal_as<String^>(hyResult->GetName());
    }

    void SegInferenceImageResults::FromHyResults(const Hy::Results& rslts)
    {
        const Hy::SegInferenceImageResults& segRslts = dynamic_cast<const Hy::SegInferenceImageResults&>(rslts);
        ImageResults = gcnew array<SegInferenceResultsProperties^>(segRslts.rs_imageResults.size());
        for (int i = 0; i < segRslts.rs_imageResults.size(); i++)
        {
            ImageResults[i] = gcnew SegInferenceResultsProperties();
            ImageResults[i]->clsId = segRslts.rs_imageResults[i].r_clsID;
            ImageResults[i]->clsName = msclr::interop::marshal_as<String^>(segRslts.rs_imageResults[i].r_clsName);
            ImageResults[i]->area = segRslts.rs_imageResults[i].r_area;

            if (ImageResults[i]->rectange == nullptr)
            {
                ImageResults[i]->rectange = (Rect^)RegionFactory::Create(Hy::RegionType::REGION_RECTANGLE);
            }

            if (ImageResults[i]->centroids == nullptr)
            {
                ImageResults[i]->centroids = (Point2f^)RegionFactory::Create(Hy::RegionType::REGION_POINT2F);
            }

            if (ImageResults[i]->contour == nullptr)
            {
                ImageResults[i]->contour = (Contour^)RegionFactory::Create(Hy::RegionType::REGION_CONTOUR);
            }

            ImageResults[i]->rectange->FromHyRect(segRslts.rs_imageResults[i].r_rectange);
            ImageResults[i]->centroids->FromHyPoint2f(segRslts.rs_imageResults[i].r_centroids);
            ImageResults[i]->contour->FromHyContour(segRslts.rs_imageResults[i].r_contour);
        }
    }
}