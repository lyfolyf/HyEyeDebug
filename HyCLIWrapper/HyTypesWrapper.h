#pragma once
#include <msclr/marshal_cppstd.h>

#include "../HyAlgorithm/Include/Basic/HyTypes.h"
#include "../HyAlgorithm/Include/Basic/HyTypeFactory.h"
#include "../HyAlgorithm/Include/ViAlgorithm/ViAlgorithm.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Drawing;

namespace HyWrapper
{
    public enum class ErrorType
    {
        OK = HY_OK,
        FAIL = HY_FAIL
    };

    [Serializable]
    public ref class Size
    {
    public:
        int width;
        int height;

    public:
        Size() : width(0), height(0) {}
        Size(int w, int h)
        {
            width = w;
            height = h;
        }
        ~Size() {}
    };

    [Serializable]
    public ref class Size2f
    {
    public:
        float width;
        float height;

    public:
        Size2f() : width(0.0f), height(0.0f) {}
        Size2f(float w, float h)
        {
            width = w;
            height = h;
        }
        ~Size2f() {}
    };

    generic <class T>
    [Serializable]
    public ref class Range
    {
    public:
        T lower;
        T upper;

        Range() {};
        Range(T low, T up)
        {
            lower = low;
            upper = up;
        };
        ~Range() {};
    };

    [Serializable]
    public ref class RangeParam
    {
    public:
        bool enable;
        String^ rangeName;
        property Range<float>^ RangeF
        {
            void set(Range<float>^ value)
            {
                range = value;
            }
            Range<float>^ get() {
                return range;
            }
        }
        RangeParam() :enable(false) {};
        ~RangeParam() {};

        Hy::RangeParam ToHyRangeParam()
        {
            Hy::RangeParam hyRangeParams;
            hyRangeParams.enable = enable;
            msclr::interop::marshal_context context;
            if (rangeName != nullptr)
            {
                hyRangeParams.rangeName = context.marshal_as<std::string>(rangeName);
            }
            hyRangeParams.range.lower = range->lower;
            hyRangeParams.range.upper = range->upper;

            return hyRangeParams;
        }

    private:
        Range<float>^ range;
    };

    [Serializable]
    public ref class RangeF32
    {
    public:
        float lower;
        float upper;

    public:
        RangeF32() {}
        RangeF32(const RangeF32^ copy)
        {
            lower = copy->lower;
            upper = copy->upper;
        }
        RangeF32(float low, float up)
        {
            lower = low;
            upper = up;
        }
    };

    [Serializable]
    public ref class Image
    {
    private:
        Hy::Image* m_img;
        bool m_ownHyImg;

    public:
        Image() : m_img(NULL) , m_ownHyImg(true) {};
        Image(int width,
            int height,
            int nChannels,
            String^ channelSeq,
            int widthStep,
            int depth);
        Image(unsigned char* bmp, bool copyData);
        Image(unsigned char* bmp);
        Image(int width,
            int height,
            int nChannels,
            String^ channelSeq,
            int widthStep,
            int depth,
            unsigned char* imgData,
            bool copyData);
        Image(int width,
            int height,
            int nChannels,
            String^ channelSeq,
            int widthStep,
            int depth,
            unsigned char* imgData);
        Image(const Image^ copy);
        Image(String^ filename);
        ~Image();
        void Release();
        Hy::Image* GetHyImage();
        Hy::Image& ToHyImage();
        void FromHyImage(const Hy::Image* hyImg);
        Bitmap^ ToBitmap();
        int Width();
        int Height();
        bool Empty();
        int Depth();
        int Channels();
        const unsigned char* Data();
        void Save(String^ filename);
        void Load(String^ filename);
    };

    public enum class RegionType
    {
        REGION_UNKNOWN = Hy::REGION_UNKNOWN,
        REGION_RECTANGLE = Hy::REGION_RECTANGLE,
        REGION_CIRCLE = Hy::REGION_CIRCLE,
        REGION_POLYGON = Hy::REGION_POLYGON,
        REGION_ROTATED_RECT = Hy::REGION_ROTATED_RECT,
        REGION_LINE = Hy::REGION_LINE,
        REGION_LINE2F = Hy::REGION_LINE2F,
        REGION_ARC = Hy::REGION_ARC,
        REGION_ANNULAR_SECTOR = Hy::REGION_ANNULAR_SECTOR,
        REGION_POINTS = Hy::REGION_POINTS,
        REGION_POINT = Hy::REGION_POINT,
        REGION_POINT2F = Hy::REGION_POINT2F,

        // Region type not supported in Hy Engine.
        REGION_CROSSMARK = 1000,
        REGION_TEXT = 1001,
        REGION_CONTOUR = 1002
    };

    public interface class Region
    {
    public:
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
    };

    [Serializable]
    public ref class Point : public Region
    {
    public:
        int x;
        int y;
        Point() {}
        Point(int px, int py);
        Point(const Point^ copy);
        ~Point() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::Point ToHyPoint();
        void FromHyPoint(Hy::Point point);
    };

    [Serializable]
    public ref class Point2f : public Region
    {
    public:
        float x;
        float y;
        Point2f() {}
        Point2f(float px, float py);
        Point2f(const Point2f^ copy);
        ~Point2f() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::Point2f ToHyPoint2f();
        void FromHyPoint2f(Hy::Point2f point);
    };

    [Serializable]
    public ref class Points : public Region
    {
    public:
        array<Point^>^ points;
        Points() {}
        Points(array<Point^>^ pts);
        Points(const Points^ copy);
        ~Points() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::Points ToHyPoints();
        void FromHyPoints(Hy::Points pts);
    };

    [Serializable]
    public ref class Line : public Region
    {
    public:
        Point^ pt1;
        Point^ pt2;

        Line() {}
        Line(Point^ p1, Point^ p2);
        Line(Line^ line);
        ~Line() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::Line ToHyLine();
        void FromHyLine(Hy::Line line);
    };

    [Serializable]
    public ref class Line2f : public Region
    {
    public:
        Point2f^ pt1;
        Point2f^ pt2;

        Line2f() { }
        Line2f(Point2f^ p1, Point2f^ p2);
        Line2f(const Line2f^ copy);
        Line2f(Line^ line);
        ~Line2f() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::Line2f ToHyLine();
        void FromHyLine(Hy::Line2f line);
    };

    [Serializable]
    public ref class Arc : public Region
    {
    public:
        Point2f^ center;
        float radius;
        float startAngle;
        float endAngle;

        Arc() : radius(0.0f) { }
        Arc(float cx, float cy, float r, float arcStartAngle, float arcEndAngle);
        ~Arc() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::Arc ToHyArc();
        void FromHyArc(Hy::Arc arc);
    };

    [Serializable]
    public ref class AnnularSector : public Region
    {
    public:
        Point2f^ center;
        float innerRadius;
        float outerRadius;
        float startAngle;
        float endAngle;

        AnnularSector() : innerRadius(0.0f), outerRadius(0.0f) { }
        AnnularSector(float cx, float cy, float asinnerRadius, float asouterRadius, float astartAngle, float asendAngle);
        ~AnnularSector() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::AnnularSector ToHyAnnularSector();
        void FromHyAnnularSector(Hy::AnnularSector annularSector);
    };

    [Serializable]
    public ref class Rect : public Region
    {
    public:
        int x;
        int y;
        int width;
        int height;

    public:
        Rect() {}
        Rect(const Rect^ copy);
        Rect(int px, int py, int pw, int ph);
        Rect(Hy::Rect rect);
        ~Rect() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        array<Point^>^ ToPoints();
        Hy::Rect ToHyRect();
        void FromHyRect(Hy::Rect rect);
    };

    [Serializable]
    public ref class RotatedRect : public Region
    {
    public:
        float angle;
        Size2f^ size;
        Point2f^ center;
    public:
        RotatedRect() : angle(0.0f) {}
        RotatedRect(Point2f^ pCenter, Size2f^ pSize, float pAngle);
        RotatedRect(Hy::RotatedRect rect);
        ~RotatedRect() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        void Points([Out] array<Point2f^>^% pts);
        Hy::RotatedRect ToHyRotatedRect();
        void FromHyRotatedRect(const Hy::RotatedRect& rect);
    };

    [Serializable]
    public ref class Circle : public Region
    {
    public:
        float radius;
        Point2f^ center;

    public:
        Circle() {}
        Circle(const Circle^ copy);
        Circle(float r, Point2f^ c);
        ~Circle() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
        Hy::Circle ToHyCircle();
        void FromHyCircle(const Hy::Circle circle);
    };

    [Serializable]
    public ref class CrossMark : public Region
    {
    public:
        Point2f^ location;
        float radius;

    public:
        CrossMark() {}
        CrossMark(const CrossMark^ copy);
        CrossMark(Point2f^ loc, float r);
        CrossMark(Point2f^ loc);
        CrossMark(Point^ loc, float r);
        CrossMark(Point^ loc);
        ~CrossMark() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
    };

    [Serializable]
    public ref class Text : public Region
    {
    public:
        Point2f^ location;
        int fontSize;
        String^ text;

    public:
        Text() {}
        Text(Point2f^ loc, int fSize, String^ txt);
        Text(const Text^ copy);
        ~Text() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);
    };

    [Serializable]
    public ref class Contour : public Region
    {
    public:
        array<Point2f^>^ vertices;

    public:
        Contour() {}
        Contour(array<Point2f^>^ pts);
        Contour(const Contour^ copy);
        ~Contour() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);

        int Size();
        Hy::Contour ToHyContour();
        void FromHyContour(const Hy::Contour& hyContour);
    };

    [Serializable]
    public ref class Polygon : public Region
    {
    public:
        array<Point^>^ vertices;

    public:
        Polygon() {}
        Polygon(const Polygon^ copy);
        Polygon(array<Point^>^ pts);
        Polygon(array<Point2f^>^ pts);
        Polygon(const Hy::Polygon& hyPoly);
        Polygon(RotatedRect^ rRect);
        ~Polygon() {}

        virtual bool IsValid();
        virtual Region^ Clone();
        virtual RegionType GetId();
        virtual void Rescale(float scale);
        virtual Hy::Region* ToHyRegion();
        virtual void FromHyRegion(const Hy::Region* region);

        int Size();
        Hy::Polygon ToHyPolygon();
        void FromHyPolygon(const Hy::Polygon& hyPoly);
        void FromRect(Rect^ rect);
    };

    [Serializable]
    public ref class RegionFactory {
    public:
        static Region^ Create(Hy::RegionType type)
        {
            Region^ region;

            switch (type)
            {
            case Hy::REGION_POINT:
                region = gcnew Point();
                break;
            case Hy::REGION_POINT2F:
                region = gcnew Point2f();
                break;
            case Hy::REGION_POINTS:
                region = gcnew Points();
                break;
            case Hy::REGION_CONTOUR:
                region = gcnew Contour();
                break;
            case Hy::REGION_RECTANGLE:
                region = gcnew Rect();
                break;
            case Hy::REGION_CIRCLE:
                region = gcnew Circle();
                break;
            case Hy::REGION_POLYGON:
                region = gcnew Polygon();
                break;
            case Hy::REGION_ROTATED_RECT:
                region = gcnew RotatedRect();
                break;
            case Hy::REGION_LINE:
                region = gcnew Line();
                break;
            case Hy::REGION_LINE2F:
                region = gcnew Line2f();
                break;
            case Hy::REGION_ARC:
                region = gcnew Arc();
                break;
            case Hy::REGION_ANNULAR_SECTOR:
                region = gcnew AnnularSector();
                break;
            default:
                throw gcnew Exception();
                break;
            }

            return region;
        }
    };
}

