// This is the main DLL file.

#include "stdafx.h"

#include "ViAlgorithmWrapper.h"
#include "SegInfWrapper.h"
#include "DetInfWrapper.h"
#include <iostream>

using namespace System;
using namespace System::Collections;

namespace HyWrapper
{
    ////////////////////////////////////////// Vision Algorithm ////////////////////////////////////////////////////
    ViAlgorithm::ViAlgorithm()
    {
        m_hyAlgorithm = nullptr;
        m_hyProperties = nullptr;
    }

    ViAlgorithm::ViAlgorithm(int algorithmType)
    {
        if (algorithmType != (int)AlgorithmType::ALGORITHM_UNKNOWN)
        {
            m_hyAlgorithm = Hy::ViAlgorithmProcess::GetViAlgorithm(algorithmType);
        }
        else
        {
            throw gcnew Exception("Wrong engine type!");
        }
        m_hyProperties = new Hy::AlgorithmProperties;
        m_hyInputs = new std::vector<const Hy::GenericInputs*>();
    }

    ViAlgorithm::~ViAlgorithm()
    {
        delete m_hyAlgorithm;
        delete m_hyProperties;
        delete m_hyInputs;
    }

    int ViAlgorithm::Initialize()
    {
        return m_hyAlgorithm->Initialize();
    }

    int ViAlgorithm::GetType()
    {
        return m_hyAlgorithm->GetType();
    }

    String^ ViAlgorithm::GetName()
    {
        return msclr::interop::marshal_as<String^>(m_hyAlgorithm->GetName());
    }

    void ViAlgorithm::SetProperties(AlgorithmProperties^ properties)
    {
        if (properties == nullptr) return;

        msclr::interop::marshal_context context;
        if (properties->id != nullptr)
        {
            m_hyProperties->id = context.marshal_as<std::string>(properties->id);
        }
        if (properties->alias != nullptr)
        {
            m_hyProperties->alias = context.marshal_as<std::string>(properties->alias);
        }

        m_hyProperties->referenceAlgorithmList.clear();
        if (properties->referenceAlgorithmList != nullptr)
        {
            for (int i = 0; i < properties->referenceAlgorithmList->Length; i++)
            {
                Hy::ReferenceAlgorithm refAlgorithm;
                refAlgorithm.algorithmId = context.marshal_as<std::string>(properties->referenceAlgorithmList[i]->preLinkAlgorithmId);
                refAlgorithm.outParamName = context.marshal_as<std::string>(properties->referenceAlgorithmList[i]->preLinkOutParamName);
                refAlgorithm.inParamName = context.marshal_as<std::string>(properties->referenceAlgorithmList[i]->inParamName);
                m_hyProperties->referenceAlgorithmList.push_back(refAlgorithm);
            }
        }

        if (properties->mode != nullptr)
        {
            m_hyProperties->mode = context.marshal_as<std::string>(properties->mode);
        }

        m_hyAlgorithm->SetProperties(*m_hyProperties);
    }

    AlgorithmProperties^ ViAlgorithm::GetProperties()
    {
        AlgorithmProperties^ properties = gcnew AlgorithmProperties;
        *m_hyProperties = m_hyAlgorithm->GetProperties();
        properties->id = msclr::interop::marshal_as<String^>(m_hyProperties->id);
        properties->alias = msclr::interop::marshal_as<String^>(m_hyProperties->alias);
        properties->mode = msclr::interop::marshal_as<String^>(m_hyProperties->mode);
        properties->predecessorId = msclr::interop::marshal_as<String^>(m_hyProperties->predecessorId);
        properties->successorId = msclr::interop::marshal_as<String^>(m_hyProperties->successorId);

        array<ReferenceAlgorithm^>^ refAlgorithmList = gcnew array<ReferenceAlgorithm^>(m_hyProperties->referenceAlgorithmList.size());
        for (int i = 0; i < m_hyProperties->referenceAlgorithmList.size(); i++)
        {
            ReferenceAlgorithm^ referenceAlgorithm = gcnew ReferenceAlgorithm();
            referenceAlgorithm->preLinkAlgorithmId = msclr::interop::marshal_as<String^>(m_hyProperties->referenceAlgorithmList[i].algorithmId);
            referenceAlgorithm->preLinkOutParamName = msclr::interop::marshal_as<String^>(m_hyProperties->referenceAlgorithmList[i].outParamName);
            referenceAlgorithm->inParamName = msclr::interop::marshal_as<String^>(m_hyProperties->referenceAlgorithmList[i].inParamName);
            refAlgorithmList[i] = referenceAlgorithm;
        }
        properties->referenceAlgorithmList = refAlgorithmList;

        return properties;
    }

    int ViAlgorithm::SetParameters(Parameters^ params)
    {
        return m_hyAlgorithm->SetParameters(params->ToHyParams());
    }

    Hy::IViAlgorithm* ViAlgorithm::GetHyAlgorithm()
    {
        return m_hyAlgorithm;
    }

    int ViAlgorithm::Process(Image^ inputImg, Rect^ inputRect, [Out] array<Results^>^% outputs)
    {
        std::vector<const Hy::Results*> hyOutputs;
        int rtn = m_hyAlgorithm->Process(inputImg->ToHyImage(), inputRect->ToHyRect(), hyOutputs);
        if (rtn == HY_OK)
        {
            HyResultsToResults(hyOutputs);
            outputs = m_outputArr;
        }
        return rtn;
    }

    int ViAlgorithm::Process(Image^ inputImg, [Out] array<Results^>^% outputs)
    {
        std::vector<const Hy::Results*> hyOutputs;
        int rtn = m_hyAlgorithm->Process(inputImg->ToHyImage(), hyOutputs);
        std::cout << "hyOutputs.size(): " << hyOutputs.size() << std::endl;
        if (rtn == HY_OK)
        {
            HyResultsToResults(hyOutputs);
            outputs = m_outputArr;
        }
        return rtn;
    }

    int ViAlgorithm::Process(array<Image^>^ inputImgs, [Out] array<Results^>^% outputs)
    {
        std::cout << "inputImgs.Length: " << inputImgs->Length << std::endl;
        std::vector<const Hy::Image*> hyImages;
        for (int i = 0; i < inputImgs->Length; i++) 
        {
            hyImages.push_back(inputImgs[i]->GetHyImage());
        }
        std::cout << "hyImages.size: " << hyImages.size() << std::endl;
        std::vector<const Hy::Results*> hyOutputs;
        int rtn = m_hyAlgorithm->Process(hyImages, hyOutputs);
        std::cout << "hyOutputs.size: " << hyOutputs.size() << std::endl;
        std::cout << "return code: " << rtn << std::endl;
        if (rtn == HY_OK)
        {
            HyResultsToResults(hyOutputs);
            outputs = m_outputArr;
        }
        return rtn;
    }

    //@XXX: inputs/outputs conversion is redundant, for vision tool chain processing we need to pass down the linked vision tools
    // directly (by using VisionToolChain class) and process them at the lower level.
    int ViAlgorithm::Process(array<GenericInputs^>^ inputs, [Out] array<Results^>^% outputs)
    {
        if (inputs->Length == 0) return HY_FAIL;

        //@XXX: We have to manually release the memory for the converted mvInputs,
        //otherwise we have memory leak.
        for (size_t i = 0; i < m_hyInputs->size(); ++i)
        {
            delete m_hyInputs->at(i);
        }
        m_hyInputs->clear();

        for (int i = 0; i < inputs->Length; ++i)
        {
            const Hy::GenericInputs& hyInput = inputs[i]->ToHyInputs();
            m_hyInputs->push_back(&hyInput);
        }

        std::vector<const Hy::Results*> hyOutputs;
        if (m_hyAlgorithm->Process(*m_hyInputs, hyOutputs) == HY_OK)
        {
            HyResultsToResults(hyOutputs);
            outputs = m_outputArr;
        }
        else
        {
            throw gcnew Exception("Processing errors!");
        }

        // Manually collect.
        System::GC::Collect();
        System::GC::WaitForPendingFinalizers();

        return HY_OK;
    }

    int ViAlgorithm::GetResults([Out] array<Results^>^% output)
    {
        std::vector<const Hy::Results*> mvOutput;
        int rtn = m_hyAlgorithm->GetResults(mvOutput);
        if (rtn == HY_OK)
        {
            HyResultsToResults(mvOutput);
            output = m_outputArr;
        }
        else
        {
            m_outputArr = gcnew array<Results^>(0);
        }

        return rtn;
    }

    void ViAlgorithm::HyResultsToResults(std::vector<const Hy::Results*>& hyOutput)
    {
        m_outputArr = gcnew array<Results^>(static_cast<int>(hyOutput.size()));
        for (int i = 0; i < static_cast<int>(hyOutput.size()); ++i)
        {
            std::string name = hyOutput[i]->GetName();
            if (name == "SegInferenceResults")
            {
                m_outputArr[i] = gcnew SegInferenceImageResults();
            }
            else if (name == "DetInferenceResults")
            {
                m_outputArr[i] = gcnew DetInferenceImageResults();
            }
            else
            {
                throw gcnew Exception("Invalid algorithm name.");
            }

            // m_outputArr[i]->Properties = this->GetProperties();
            m_outputArr[i]->FromHyResults(*hyOutput[i]);
        }
    }

    ///////////////////////////////////////////////Vision Engine Chain///////////////////////////////////////////

    ViAlgorithmChain::ViAlgorithmChain()
    {
    }

    ViAlgorithmChain::~ViAlgorithmChain()
    {
        delete m_hyInputs;
    }

    int ViAlgorithmChain::Process(Image^ inputImg, array<ViAlgorithm^>^ algorithms, [Out] array<Results^>^% outputs)
    {
        std::vector<Hy::IViAlgorithm*> algorithmList;
        for (int i = 0; i < algorithms->Length; ++i)
        {
            if (algorithms[i] == nullptr) break;
            if (algorithms[i]->GetName() != "ResultsManipulator")
            {
                algorithmList.push_back(algorithms[i]->GetHyAlgorithm());
            }
        }

        std::vector<const Hy::Results*> hyOutputs;
        int rtn = Hy::ViAlgorithmProcess::ProcessChain(inputImg->ToHyImage(), algorithmList, hyOutputs);

        if (rtn == HY_OK)
        {
            HyResultsToResults(hyOutputs);
            outputs = m_outputArr;

            if ((algorithms[algorithms->Length - 1] != nullptr) && (algorithms[algorithms->Length - 1]->GetName() == "ResultsManipulator"))
            {
                array<GenericInputs^>^ inputs = gcnew array<GenericInputs^>(outputs->Length);
                for (int i = 0; i < outputs->Length; ++i)
                {
                    inputs[i] = outputs[i];
                }
                algorithms[algorithms->Length - 1]->Process(inputs, outputs);
            }

            return HY_OK;
        }
        else
        {
            return HY_FAIL;
        }
    }

    void ViAlgorithmChain::HyResultsToResults(std::vector<const Hy::Results*>& hyOutput)
    {
        m_outputArr = gcnew array<Results^>(static_cast<int>(hyOutput.size()));
        for (int i = 0; i < static_cast<int>(hyOutput.size()); ++i)
        {
            std::string name = hyOutput[i]->GetName();
            if (name == "Matching")
            {
                // TODO: m_outputArr[i] = gcnew MatchingResults();
            }
            else if (name == "Measuring")
            {
                // TODO: m_outputArr[i] = gcnew MeasuringResults();
            }
            else
            {
                throw gcnew Exception("Invalid algorithm name.");
            }

            m_outputArr[i]->FromHyResults(*hyOutput[i]);
            m_outputArr[i]->id = Convert::ToString(i);
        }
    }
}
