#include "Stdafx.h"
#include "HyTypesWrapper.h"
namespace HyWrapper
{
    /////////////////////////////////////////// Image //////////////////////////////////////////
    Image::Image(int width,
        int height,
        int nChannels,
        String^ channelSeq,
        int widthStep,
        int depth) : m_img(NULL) , m_ownHyImg(true)
    {
        msclr::interop::marshal_context context;
        std::string chSeq = context.marshal_as<std::string>(channelSeq);
        m_img = new Hy::Image(width, height, nChannels, chSeq.c_str(),
            widthStep, depth);
    }

    Image::Image(unsigned char* bmp, bool copyData) : m_img(NULL) , m_ownHyImg(true)
    {
        m_img = new Hy::Image();
        m_img->FromBitmap(bmp, copyData);
    }

    Image::Image(unsigned char* bmp)
    {
        Image(bmp, false);
    }

    Image::Image(int width,
        int height,
        int nChannels,
        String^ channelSeq,
        int widthStep,
        int depth,
        unsigned char* imgData,
        bool copyData) : m_img(NULL) , m_ownHyImg(true)
    {
        msclr::interop::marshal_context context;
        std::string chSeq = context.marshal_as<std::string>(channelSeq);
        m_img = new Hy::Image(width, height, nChannels, chSeq.c_str(),
            widthStep, depth, imgData, copyData);
    }

    Image::Image(int width,
        int height,
        int nChannels,
        String^ channelSeq,
        int widthStep,
        int depth,
        unsigned char* imgData)
    {
        Image(width, height, nChannels, channelSeq,
            widthStep, depth, imgData, false);
    }

    Image::Image(const Image^ copy) : m_img(NULL) , m_ownHyImg(true)
    {
        m_img = new Hy::Image(*copy->m_img);
    }

    Image::Image(String^ filename) : m_img(NULL) , m_ownHyImg(true)
    {
        msclr::interop::marshal_context context;
        m_img = new Hy::Image(context.marshal_as<std::string>(filename));
    }

    Image::~Image()
    {
        Release();
    }

    void Image::Release()
    {
        //Only release Hy::Image allocated in the Wrapper;
        if (m_ownHyImg)
        {
            delete m_img;
            m_img = nullptr;
        }
    }

    Hy::Image* Image::GetHyImage() 
    {
        return m_img;
    }

    Hy::Image& Image::ToHyImage()
    {
        return *m_img;
    }

    void Image::FromHyImage(const Hy::Image* hyImg)
    {
        Release();
        m_img = const_cast<Hy::Image*>(hyImg);
        //Not self-allocated Hy::Image
        m_ownHyImg = false;
    }

    Bitmap^ Image::ToBitmap()
    {
        if (m_img == NULL)
        {
            throw gcnew Exception("Null m_img.");
        }

        Imaging::PixelFormat pixelFmt = Imaging::PixelFormat::Undefined;
        if (m_img->Depth() != 8)
        {
            //@TODO: Implement code for different depth.
            throw gcnew Exception("The depth is not supported!");
        }

        switch (m_img->Channels())
        {
        case 1:
            pixelFmt = Imaging::PixelFormat::Format8bppIndexed;
            break;
        case 3:
            pixelFmt = Imaging::PixelFormat::Format24bppRgb;
            break;
        case 4:
            pixelFmt = Imaging::PixelFormat::Format32bppRgb;
            break;
        default:
            throw gcnew Exception("The number of channel is not supported!");
            break;
        }

        Bitmap^ bmp = gcnew Bitmap(m_img->Width(), m_img->Height(), m_img->WidthStep(), pixelFmt, IntPtr((void*)m_img->Data()));

        if (pixelFmt == Imaging::PixelFormat::Format8bppIndexed)
        {
            //get and fillup a grayscale-palette
            Imaging::ColorPalette^ plt = bmp->Palette;
            for (int i = 0; i < 256; ++i)
            {
                plt->Entries[i] = Color::FromArgb(i, i, i);
            }
            bmp->Palette = plt;
        }
        return bmp;
    }

    int Image::Width()
    {
        if (m_img == NULL) return -1;
        return m_img->Width();
    }

    int Image::Height()
    {
        if (m_img == NULL) return -1;
        return m_img->Height();
    }

    bool Image::Empty()
    {
        if (m_img == NULL) return true;
        if (m_img->Data() == NULL) return true;
        return false;
    }

    int Image::Depth()
    {
        if (m_img == NULL) return -1;
        return m_img->Depth();
    }

    int Image::Channels()
    {
        if (m_img == NULL) return -1;
        return m_img->Channels();
    }

    const unsigned char* Image::Data()
    {
        if (m_img == NULL) return NULL;
        return m_img->Data();
    }

    void Image::Save(String^ filename)
    {
        msclr::interop::marshal_context context;
        m_img->Save(context.marshal_as<std::string>(filename));
    }

    void Image::Load(String^ filename)
    {
        msclr::interop::marshal_context context;
        m_img->Load(context.marshal_as<std::string>(filename));
    }

    /////////////////////////////////////////// Region //////////////////////////////////////////

    ////////////////////// Region Of Point ////////////////////
    Point::Point(int px, int py)
    {
        x = px;
        y = py;
    }

    Point::Point(const Point^ copy)
    {
        x = copy->x;
        y = copy->y;
    }

    bool Point::IsValid()
    {
        return true;
    }

    Region^  Point::Clone()
    {
        return gcnew Point(this);
    }

    RegionType  Point::GetId()
    {
        return RegionType::REGION_POINT;
    }

    void Point::Rescale(float scale)
    {
        x = x * scale;
        y = y * scale;
    }

    Hy::Region* Point::ToHyRegion()
    {
        Hy::Point* region = dynamic_cast<Hy::Point*>(Hy::CRegionFactory::Create(Hy::REGION_POINT));
        *region = ToHyPoint();
        return region;
    }

    void Point::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Point& point = dynamic_cast<const Hy::Point&>(*region);
        FromHyPoint(point);
    }

    Hy::Point Point::ToHyPoint()
    {
        Hy::Point point(x, y);
        return point;
    }

    void Point::FromHyPoint(Hy::Point point)
    {
        x = point.x;
        y = point.y;
    }

    ////////////////////// Region Of Point2f ////////////////////
    Point2f::Point2f(float px, float py)
    {
        x = px;
        y = py;
    }

    Point2f::Point2f(const Point2f^ copy)
    {
        x = copy->x;
        y = copy->y;
    }

    bool Point2f::IsValid()
    {
        return true;
    }

    Region^  Point2f::Clone()
    {
        return gcnew Point2f(this);
    }

    RegionType  Point2f::GetId()
    {
        return RegionType::REGION_POINT2F;
    }

    void Point2f::Rescale(float scale)
    {
        x = x * scale;
        y = y * scale;
    }

    Hy::Region* Point2f::ToHyRegion()
    {
        Hy::Point2f* region = dynamic_cast<Hy::Point2f*>(Hy::CRegionFactory::Create(Hy::REGION_POINT2F));
        *region = ToHyPoint2f();
        return region;
    }

    void Point2f::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Point2f& point = dynamic_cast<const Hy::Point2f&>(*region);
        FromHyPoint2f(point);
    }

    Hy::Point2f Point2f::ToHyPoint2f()
    {
        Hy::Point2f point(x, y);
        return point;
    }

    void Point2f::FromHyPoint2f(Hy::Point2f point)
    {
        x = point.x;
        y = point.y;
    }

    ////////////////////// Region Of Points ////////////////////
    Points::Points(array<Point^>^ pts)
    {
        points = gcnew array<Point^>(pts->Length);
        for (int i = 0; i < points->Length; ++i)
        {
            points[i] = gcnew Point(pts[i]);
        }
    }

    Points::Points(const Points^ copy)
    {
        points = gcnew array<Point^>(copy->points->Length);
        for (int i = 0; i < points->Length; ++i)
        {
            points[i] = gcnew Point(copy->points[i]);
        }
    }

    bool Points::IsValid()
    {
        bool rst = false;
        if (points == nullptr)
            return rst;

        for each (Point^ pt in points)
        {
            if (pt == nullptr)
                return rst;

            if (pt->x > 0 || pt->y > 0)
                rst = true;
        }

        return rst;
    }

    Region^ Points::Clone()
    {
        return gcnew Points(this->points);
    }

    RegionType Points::GetId()
    {
        return RegionType::REGION_POINTS;
    }

    void Points::Rescale(float scale)
    {
        for (int i = 0; i < points->Length; ++i)
        {
            points[i]->x *= scale;
            points[i]->y *= scale;
        }
    }

    Hy::Region* Points::ToHyRegion()
    {
        Hy::Points* region = dynamic_cast<Hy::Points*>(Hy::CRegionFactory::Create(Hy::REGION_POINTS));
        *region = ToHyPoints();
        return region;
    }

    void Points::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Points& hyPoints = dynamic_cast<const Hy::Points&>(*region);
        FromHyPoints(hyPoints);
    }

    Hy::Points Points::ToHyPoints()
    {
        std::vector<Hy::Point> pts;
        for (int i = 0; i < points->Length; ++i)
        {
            Hy::Point pt(points[i]->x, points[i]->y);
            pts.push_back(pt);
        }

        Hy::Points hyPoints(pts);
        return hyPoints;
    }

    void Points::FromHyPoints(Hy::Points hyPoints)
    {
        points = gcnew cli::array<Point^>(hyPoints.points.size());
        for (int i = 0; i < points->Length; ++i)
        {
            points[i] = gcnew Point(hyPoints.points[i].x, hyPoints.points[i].y);
        }
    }

    ////////////////////// Region Of Line ////////////////////
    Line::Line(Point^ p1, Point^ p2)
    {
        pt1 = p1;
        pt2 = p2;
    }

    Line::Line(Line^ line)
    {
        pt1 = gcnew Point(line->pt1->x, line->pt1->y);
        pt2 = gcnew Point(line->pt2->x, line->pt2->y);
    }

    bool Line::IsValid()
    {
        if (pt1 == nullptr || pt2 == nullptr)
            return false;

        if (pt1->x == 0 && pt1->y == 0 && pt2->x == 0 && pt2->y == 0)
            return false;

        return true;
    }

    Region^  Line::Clone()
    { 
        return gcnew Line(this);
    }

    RegionType  Line::GetId()
    { 
        return RegionType::REGION_LINE;
    }

    void Line::Rescale(float scale)
    {
        pt1->x = (int)(pt1->x * scale);
        pt1->y = (int)(pt1->y * scale);
        pt2->x = (int)(pt2->x * scale);
        pt2->y = (int)(pt2->y * scale);
    }

    Hy::Region* Line::ToHyRegion()
    {
        Hy::Line* region = dynamic_cast<Hy::Line*>(Hy::CRegionFactory::Create(Hy::REGION_LINE));
        *region = ToHyLine();
        return region;
    }

    void Line::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Line& line = dynamic_cast<const Hy::Line&>(*region);
        FromHyLine(line);
    }

    Hy::Line Line::ToHyLine()
    {
        Hy::Point point1(pt1->x, pt1->y);
        Hy::Point point2(pt2->x, pt2->y);
        Hy::Line line(point1, point2);
        return line;
    }

    void Line::FromHyLine(Hy::Line line)
    {
        pt1 = gcnew Point(line.pt1.x, line.pt1.y);
        pt2 = gcnew Point(line.pt2.x, line.pt2.y);
    }

    ////////////////////// Region Of Line2f ////////////////////
    Line2f::Line2f(Point2f^ p1, Point2f^ p2)
    {
        pt1 = p1;
        pt2 = p2;
    }

    Line2f::Line2f(const Line2f^ copy)
    {
        pt1 = gcnew Point2f(copy->pt1);
        pt2 = gcnew Point2f(copy->pt2);
    }

    Line2f::Line2f(Line^ line)
    {
        pt1 = gcnew Point2f(line->pt1->x, line->pt1->y);
        pt2 = gcnew Point2f(line->pt2->x, line->pt2->y);
    }

    bool Line2f::IsValid()
    {
        if (pt1 == nullptr || pt2 == nullptr)
            return false;

        if (pt1->x == 0 && pt1->y == 0 && pt2->x == 0 && pt2->y == 0)
            return false;

        return true;
    }

    Region^ Line2f::Clone()
    { 
        return gcnew Line2f(this);
    }

    RegionType Line2f::GetId()
    { 
        return RegionType::REGION_LINE2F;
    }

    void Line2f::Rescale(float scale)
    {
        pt1->x *= scale;
        pt1->y *= scale;
        pt2->x *= scale;
        pt2->y *= scale;
    }

    Hy::Region* Line2f::ToHyRegion()
    {
        Hy::Line2f* region = dynamic_cast<Hy::Line2f*>(Hy::CRegionFactory::Create(Hy::REGION_LINE2F));
        *region = ToHyLine();
        return region;
    }

    void Line2f::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Line2f& line = dynamic_cast<const Hy::Line2f&>(*region);
        FromHyLine(line);
    }

    Hy::Line2f Line2f::ToHyLine()
    {
        Hy::Point2f point1(pt1->x, pt1->y);
        Hy::Point2f point2(pt2->x, pt2->y);
        Hy::Line2f line(point1, point2);
        return line;
    }

    void Line2f::FromHyLine(Hy::Line2f line)
    {
        pt1 = gcnew Point2f(line.pt1.x, line.pt1.y);
        pt2 = gcnew Point2f(line.pt2.x, line.pt2.y);
    }

    ////////////////////// Region Of Arc ////////////////////
    Arc::Arc(float cx, float cy, float r, float arcStartAngle, float arcEndAngle)
    {
        center = gcnew Point2f(cx, cy);
        radius = r;
        startAngle = arcStartAngle;
        endAngle = arcEndAngle;
    }

    bool Arc::IsValid()
    {
        if (center == nullptr)
            return false;

        if (radius == 0 || startAngle == endAngle)
            return false;

        return true;
    }

    Region^ Arc::Clone()
    { 
        return gcnew Arc(this->center->x, this->center->y, this->radius, this->startAngle, this->endAngle);
    }

    RegionType Arc::GetId()
    {
        return RegionType::REGION_ARC;
    }

    void Arc::Rescale(float scale)
    {
        center->x *= scale;
        center->y *= scale;
        radius *= scale;
    }

    Hy::Region* Arc::ToHyRegion()
    {
        Hy::Arc* region = dynamic_cast<Hy::Arc*>(Hy::CRegionFactory::Create(Hy::REGION_ARC));
        *region = ToHyArc();
        return region;
    }

    void Arc::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Arc& arc = dynamic_cast<const Hy::Arc&>(*region);
        FromHyArc(arc);
    }

    Hy::Arc Arc::ToHyArc()
    {
        Hy::Arc arc(center->x, center->y, radius, startAngle, endAngle);
        return arc;
    }

    void Arc::FromHyArc(Hy::Arc arc)
    {
        center = gcnew Point2f(arc.center.x, arc.center.y);
        radius = arc.radius;
        startAngle = arc.startAngle;
        endAngle = arc.endAngle;
    }

    ////////////////////// Region Of AnnularSector ////////////////////
    AnnularSector::AnnularSector(float cx, float cy, float asinnerRadius, float asouterRadius, float astartAngle, float asendAngle)
    {
        center = gcnew Point2f(cx, cy);
        innerRadius = asinnerRadius;
        outerRadius = asouterRadius;
        startAngle = astartAngle;
        endAngle = asendAngle;
    }

    bool AnnularSector::IsValid()
    {
        if (center == nullptr)
            return false;

        if (innerRadius == 0 || outerRadius == 0 || startAngle == endAngle)
            return false;

        return true;
    }

    Region^ AnnularSector::Clone()
    { 
        return gcnew AnnularSector(this->center->x, this->center->y, this->innerRadius, this->outerRadius, this->startAngle, this->endAngle);
    }

    RegionType AnnularSector::GetId()
    { 
        return RegionType::REGION_ANNULAR_SECTOR;
    }

    void AnnularSector::Rescale(float scale)
    {
        center->x *= scale;
        center->y *= scale;
        innerRadius *= scale;
        outerRadius *= scale;
    }

    Hy::Region* AnnularSector::ToHyRegion()
    {
        Hy::AnnularSector* region = dynamic_cast<Hy::AnnularSector*>(Hy::CRegionFactory::Create(Hy::REGION_ANNULAR_SECTOR));
        *region = ToHyAnnularSector();
        return region;
    }

    void AnnularSector::FromHyRegion(const Hy::Region* region)
    {
        const Hy::AnnularSector& annularSector = dynamic_cast<const Hy::AnnularSector&>(*region);
        FromHyAnnularSector(annularSector);
    }

    Hy::AnnularSector AnnularSector::ToHyAnnularSector()
    {
        Hy::AnnularSector annularSector(center->x, center->y, innerRadius, outerRadius, startAngle, endAngle);
        return annularSector;
    }

    void AnnularSector::FromHyAnnularSector(Hy::AnnularSector annularSector)
    {
        center = gcnew Point2f(annularSector.center.x, annularSector.center.y);
        innerRadius = annularSector.innerRadius;
        outerRadius = annularSector.outerRadius;
        startAngle = annularSector.startAngle;
        endAngle = annularSector.endAngle;
    }

    ////////////////////// Region Of Rect ////////////////////
    Rect::Rect(const Rect^ copy)
    {
        x = copy->x;
        y = copy->y;
        width = copy->width;
        height = copy->height;
    }
    Rect::Rect(int px, int py, int pw, int ph)
    {
        x = px;
        y = py;
        width = pw;
        height = ph;
    }

    Rect::Rect(Hy::Rect rect)
    {
        FromHyRect(rect);
    }

    bool Rect::IsValid()
    {
        if (width == 0 || height == 0)
            return false;

        return true;
    }

    Region^ Rect::Clone()
    { 
        return gcnew Rect(this->x, this->y, this->width, this->height);
    }

    RegionType Rect::GetId()
    {
        return RegionType::REGION_RECTANGLE;
    }

    void Rect::Rescale(float scale)
    {
        x = (int)(x * scale);
        y = (int)(y * scale);
        width = (int)(width * scale);
        height = (int)(height * scale);
    }

    Hy::Region* Rect::ToHyRegion()
    {
        Hy::Rect* region = dynamic_cast<Hy::Rect*>(Hy::CRegionFactory::Create(Hy::REGION_RECTANGLE));
        *region = ToHyRect();
        return region;
    }

    void Rect::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Rect& rect = dynamic_cast<const Hy::Rect&>(*region);
        FromHyRect(rect);
    }

    array<Point^>^ Rect::ToPoints()
    {
        array<Point^>^ pts = gcnew array<Point^>(4);
        pts[0] = gcnew Point(x, y);
        pts[1] = gcnew Point(x + width - 1, y);
        pts[2] = gcnew Point(x + width - 1, y + height - 1);
        pts[3] = gcnew Point(x, y + height - 1);

        return pts;
    }

    Hy::Rect Rect::ToHyRect()
    {
        Hy::Rect rect(x, y, width, height);
        return rect;
    }
    void Rect::FromHyRect(Hy::Rect rect)
    {
        x = rect.x;
        y = rect.y;
        width = rect.width;
        height = rect.height;
    }

    ////////////////////// Region Of RotatedRect ////////////////////
    RotatedRect::RotatedRect(Point2f^ pCenter, Size2f^ pSize, float pAngle)
    {
        center = gcnew Point2f(pCenter->x, pCenter->y);
        size = gcnew Size2f(pSize->width, pSize->height);
        angle = pAngle;
    }

    RotatedRect::RotatedRect(Hy::RotatedRect rect)
    {
        FromHyRotatedRect(rect);
    }

    bool RotatedRect::IsValid()
    {
        if (center == nullptr || size == nullptr)
            return false;

        if (size->width == 0 || size->height == 0)
            return false;

        return true;
    }

    Region^ RotatedRect::Clone()
    { 
        return gcnew RotatedRect(this->center, this->size, this->angle);
    }

    RegionType RotatedRect::GetId()
    {
        return RegionType::REGION_ROTATED_RECT;
    }

    void RotatedRect::Rescale(float scale)
    {
        center->x *= scale;
        center->y *= scale;
        size->width *= scale;
        size->height *= scale;
    }

    Hy::Region* RotatedRect::ToHyRegion()
    {
        Hy::RotatedRect* region = dynamic_cast<Hy::RotatedRect*>(Hy::CRegionFactory::Create(Hy::REGION_ROTATED_RECT));
        *region = ToHyRotatedRect();
        return region;
    }

    void RotatedRect::FromHyRegion(const Hy::Region* region)
    {
        const Hy::RotatedRect& rect = dynamic_cast<const Hy::RotatedRect&>(*region);
        FromHyRotatedRect(rect);
    }

    void RotatedRect::Points([Out] array<Point2f^>^% pts)
    {
        Hy::Point2f hyPts[4];
        Hy::RotatedRect hyRect = ToHyRotatedRect();
        hyRect.Points(hyPts);
        pts = gcnew array<Point2f^>(4);
        for (int i = 0; i < 4; ++i)
        {
            pts[i] = gcnew Point2f(hyPts[i].x, hyPts[i].y);
        }
    }

    Hy::RotatedRect RotatedRect::ToHyRotatedRect()
    {
        Hy::Point2f pt(center->x, center->y);
        Hy::Size2f sz(size->width, size->height);
        Hy::RotatedRect rect(pt, sz, angle);
        return rect;
    }

    void RotatedRect::FromHyRotatedRect(const Hy::RotatedRect& rect)
    {
        angle = rect.angle;
        size = gcnew Size2f(rect.size.width, rect.size.height);
        center = gcnew Point2f(rect.center.x, rect.center.y);
    }

    ////////////////////// Region Of Circle ////////////////////
    Circle::Circle(const Circle^ copy)
    {
        radius = copy->radius;
        center = gcnew Point2f(copy->center);
    }

    Circle::Circle(float r, Point2f^ c)
    {
        radius = r;
        center = gcnew Point2f(c->x, c->y);
    }

    bool Circle::IsValid()
    {
        if (center == nullptr)
            return false;

        if (radius == 0)
            return false;

        return true;
    }

    Region^ Circle::Clone()
    {
        return gcnew Circle(this->radius, this->center);
    }

    RegionType Circle::GetId()
    {
        return RegionType::REGION_CIRCLE;
    }

    void Circle::Rescale(float scale)
    {
        center->x *= scale;
        center->y *= scale;
        radius *= scale;
    }

    Hy::Region* Circle::ToHyRegion()
    {
        Hy::Circle* region = dynamic_cast<Hy::Circle*>(Hy::CRegionFactory::Create(Hy::REGION_CIRCLE));
        *region = ToHyCircle();
        return region;
    }

    void Circle::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Circle& circle = dynamic_cast<const Hy::Circle&>(*region);
        FromHyCircle(circle);
    }

    Hy::Circle Circle::ToHyCircle()
    {
        Hy::Circle circle(center->x, center->y, radius);
        return circle;
    }

    void Circle::FromHyCircle(const Hy::Circle circle)
    {
        radius = circle.radius;
        center = gcnew Point2f(circle.center.x, circle.center.y);
    }

    ////////////////////// Region Of CrossMark ////////////////////
    CrossMark::CrossMark(const CrossMark^ copy)
    {
        location = gcnew Point2f(copy->location);
        radius = copy->radius;
    }

    CrossMark::CrossMark(Point2f^ loc, float r)
    {
        location = gcnew Point2f(loc->x, loc->y);
        radius = r;
    }

    CrossMark::CrossMark(Point2f^ loc)
    {
        location = loc;
        radius = 3.0f;
    }

    CrossMark::CrossMark(Point^ loc, float r)
    {
        location = gcnew Point2f(loc->x, loc->y);
        radius = r;
    }

    CrossMark::CrossMark(Point^ loc)
    {
        location = gcnew Point2f(loc->x, loc->y);
        radius = 3.0f;
    }

    bool CrossMark::IsValid()
    {
        if (location == nullptr)
            return false;

        return true;
    }

    Region^ CrossMark::Clone()
    {
        return gcnew CrossMark(this->location, this->radius);
    }

    RegionType CrossMark::GetId()
    {
        return RegionType::REGION_CROSSMARK;
    }

    void CrossMark::Rescale(float scale)
    {
        location->x *= scale;
        location->y *= scale;
        radius *= scale;
    }

    Hy::Region* CrossMark::ToHyRegion()
    {
        throw gcnew Exception("Hy::CrossMark not implemented!");
        Hy::Region* region = nullptr;
        return region;
    }

    void CrossMark::FromHyRegion(const Hy::Region* region)
    {
        throw gcnew Exception("Hy::CrossMark not implemented!");
    }

    ////////////////////// Region Of Text ////////////////////
    Text::Text(Point2f^ loc, int fSize, String^ txt)
    {
        location = gcnew Point2f(loc->x, loc->y);
        fontSize = fSize;
        text = gcnew String(txt);
    }

    Text::Text(const Text^ copy)
    {
        location = gcnew Point2f(copy->location);
        fontSize = copy->fontSize;
        String^ txt = copy->text;
        text = gcnew String(txt);
    }

    bool Text::IsValid()
    {
        if (location == nullptr)
            return false;

        if (fontSize == 0 || text->Length == 0)
            return false;

        return true;
    }

    Region^ Text::Clone()
    {
        return gcnew Text(this->location, this->fontSize, this->text); 
    }

    RegionType Text::GetId()
    {
        return RegionType::REGION_TEXT;
    }

    void Text::Rescale(float scale)
    {
        location->x *= scale;
        location->y *= scale;
        //@FIXME: font size may not be scaled correctly.
        fontSize = (int)(fontSize * scale);
    }

    Hy::Region* Text::ToHyRegion()
    {
        throw gcnew Exception("Hy::Text not implemented!");
        Hy::Region* region = nullptr;
        return region;
    }

    void Text::FromHyRegion(const Hy::Region* region)
    {
        throw gcnew Exception("Hy::Text not implemented!");
    }

    ////////////////////// Region Of Contour ////////////////////
    Contour::Contour(array<Point2f^>^ pts)
    {
        vertices = gcnew array<Point2f^>(pts->Length);
        for (int i = 0; i < vertices->Length; ++i)
        {
            vertices[i] = gcnew Point2f(pts[i]);
        }
    }

    Contour::Contour(const Contour^ copy)
    {
        vertices = gcnew array<Point2f^>(copy->vertices->Length);
        for (int i = 0; i < vertices->Length; ++i)
        {
            vertices[i] = gcnew Point2f(copy->vertices[i]);
        }
    }

    bool Contour::IsValid()
    {
        bool rst = false;
        if (vertices == nullptr)
            return rst;

        for each (Point2f^ pt in vertices)
        {
            if (pt == nullptr)
                return rst;

            if (pt->x > 0 || pt->y > 0)
                rst = true;
        }

        return rst;
    }

    Region^ Contour::Clone()
    {
        return gcnew Contour(this->vertices);
    }

    RegionType Contour::GetId()
    { 
        return RegionType::REGION_CONTOUR;
    }

    void Contour::Rescale(float scale)
    {
        for (int i = 0; i < vertices->Length; ++i)
        {
            vertices[i]->x *= scale;
            vertices[i]->y *= scale;
        }
    }

    Hy::Region* Contour::ToHyRegion()
    {
        Hy::Contour* region = dynamic_cast<Hy::Contour*>(Hy::CRegionFactory::Create(Hy::REGION_CONTOUR));
        *region = ToHyContour();
        return region;
    }

    void Contour::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Contour& contour = dynamic_cast<const Hy::Contour&>(*region);
        FromHyContour(contour);
    }

    int Contour::Size()
    {
        return vertices->Length;
    }

    Hy::Contour Contour::ToHyContour()
    {
        std::vector<Hy::Point2f> hyPts;
        for (int i = 0; i < vertices->Length; ++i)
        {
            Hy::Point2f pt(vertices[i]->x, vertices[i]->y);
            hyPts.push_back(pt);
        }
        Hy::Contour contour(hyPts);

        return contour;
    }

    void Contour::FromHyContour(const Hy::Contour& hyContour)
    {
        vertices = gcnew array<Point2f^>(hyContour.vertices.size());
        for (int i = 0; i < vertices->Length; ++i)
        {
            vertices[i] = gcnew Point2f(hyContour.vertices[i].x, hyContour.vertices[i].y);
        }
    }

    ////////////////////// Region Of Polygon ////////////////////
    Polygon::Polygon(const Polygon^ copy)
    {
        vertices = gcnew array<Point^>(copy->vertices->Length);
        for (int i = 0; i < vertices->Length; ++i)
        {
            vertices[i] = gcnew Point(copy->vertices[i]);
        }
    }

    Polygon::Polygon(array<Point^>^ pts)
    {
        vertices = pts;
    }

    Polygon::Polygon(array<Point2f^>^ pts)
    {
        vertices = gcnew array<Point^>(pts->Length);
        for (int i = 0; i < pts->Length; ++i)
        {
            vertices[i] = gcnew Point(pts[i]->x, pts[i]->y);
        }
    }

    Polygon::Polygon(const Hy::Polygon& hyPoly)
    {
        FromHyPolygon(hyPoly);
    }

    Polygon::Polygon(RotatedRect^ rRect)
    {
        array<Point2f^>^ pts;
        rRect->Points(pts);
        vertices = gcnew array<Point^>(pts->Length);
        for (int i = 0; i < pts->Length; ++i)
        {
            vertices[i] = gcnew Point(pts[i]->x, pts[i]->y);
        }
    }

    bool Polygon::IsValid()
    {
        bool rst = false;
        if (vertices == nullptr)
            return rst;

        for each (Point^ pt in vertices)
        {
            if (pt == nullptr)
                return rst;

            if (pt->x > 0 || pt->y > 0)
                rst = true;
        }

        return rst;
    }

    Region^ Polygon::Clone()
    {
        return gcnew Polygon(this->vertices);
    }

    RegionType Polygon::GetId()
    { 
        return RegionType::REGION_POLYGON; 
    }

    void Polygon::Rescale(float scale)
    {
        for (int i = 0; i < vertices->Length; ++i)
        {
            vertices[i]->x = (int)(vertices[i]->x * scale);
            vertices[i]->y = (int)(vertices[i]->y * scale);
        }
    }

    Hy::Region* Polygon::ToHyRegion()
    {
        Hy::Polygon* region = dynamic_cast<Hy::Polygon*>(Hy::CRegionFactory::Create(Hy::REGION_POLYGON));
        *region = ToHyPolygon();
        return region;
    }

    void Polygon::FromHyRegion(const Hy::Region* region)
    {
        const Hy::Polygon& polygon = dynamic_cast<const Hy::Polygon&>(*region);
        FromHyPolygon(polygon);
    }

    int Polygon::Size()
    {
        return vertices->Length; 
    }

    Hy::Polygon Polygon::ToHyPolygon()
    {
        std::vector<Hy::Point> hyPts;
        for (int i = 0; i < vertices->Length; ++i)
        {
            Hy::Point pt(vertices[i]->x, vertices[i]->y);
            hyPts.push_back(pt);
        }
        Hy::Polygon polygon(hyPts);

        return polygon;
    }

    void Polygon::FromHyPolygon(const Hy::Polygon& hyPoly)
    {
        vertices = gcnew array<Point^>(hyPoly.vertices.size());
        for (int i = 0; i < vertices->Length; ++i)
        {
            vertices[i] = gcnew Point(hyPoly.vertices[i].x, hyPoly.vertices[i].y);
        }
    }

    void Polygon::FromRect(Rect^ rect)
    {
        vertices = gcnew array<Point^>(4);
        Point br(rect->x + rect->width - 1, rect->y + rect->height - 1);
        vertices[0] = gcnew Point(rect->x, rect->y);
        vertices[1] = gcnew Point(br.x, rect->y);
        vertices[2] = gcnew Point(br.x, br.y);
        vertices[3] = gcnew Point(rect->x, br.y);
    }
}
