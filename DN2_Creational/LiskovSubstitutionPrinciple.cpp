#include "main.h"

//Objects in a program shoud be replaceble with instances of their subtypes w/o altering the correctness of the program

class Rectangle
{
protected:
	int width_;
	int height_;
public:
	virtual ~Rectangle() = default;

	Rectangle(const int width, const int height)
		: width_(width),
		  height_(height)
	{
	}

	virtual int width() const;
	virtual void width(const int width);
	virtual int height() const;
	virtual void height(const int height);

	int Area() const;
};

int Rectangle::width() const
{
	return width_;
}

void Rectangle::width(const int width)
{
	width_ = width;
}

int Rectangle::height() const
{
	return height_;
}

void Rectangle::height(const int height)
{
	height_ = height;
}

int Rectangle::Area() const
{
	return width_ * height_;
}

void proccess(Rectangle& r)
{
	int w = r.width();
	r.height(10);

	std::cout << "expect area = " << (w * 10)
		<< " , got " << r.Area() << std::endl;
}

struct RectangleFactory
{
	static Rectangle CreateRectangle(int w, int h);
	static Rectangle CreateSquare(int size);
};

class Square : public Rectangle
{
public:
	Square(int size) : Rectangle{size, size}{}

	void width(const int width) override;
	void height(const int height) override;
};

void Square::width(const int width)
{
	this->width_ = height_ = width;
}

void Square::height(const int height)
{
	this->height_ = width_ = height;
}

int main()
{
	Rectangle r{ 5,5 };
	proccess(r);

	/*Square s{ 5 };
	proccess(s);*/

	
	getchar();
	return 0;
}
