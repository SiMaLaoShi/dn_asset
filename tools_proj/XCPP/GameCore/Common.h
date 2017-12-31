#ifndef __Common__
#define __Common__

#if defined(__CYGWIN32__)
#define ENGINE_INTERFACE_EXPORT __declspec(dllexport)
#elif defined(WIN32) || defined(_WIN32) || defined(__WIN32__) || defined(_WIN64) || defined(WINAPI_FAMILY)
#define ENGINE_INTERFACE_EXPORT __declspec(dllexport)
#elif defined(__MACH__) || defined(__ANDROID__) || defined(__linux__) || defined(__QNX__)
#define ENGINE_INTERFACE_EXPORT
#else
#define ENGINE_INTERFACE_EXPORT
#endif

#include <sstream> 
#include <string>
#include "picojson.h"
#include "CommandDef.h"
#include <fstream>
#include<stdlib.h>

typedef unsigned int  uint;
typedef unsigned long long ulong;
typedef picojson::value object;

#define MaxStringSize 64
#define MaxArraySize 16
#define Random(min,max) (rand() % max + min) //[min,max)

extern std::string UNITY_STREAM_PATH;
extern std::string UNITY_CACHE_PATH;
static int id = 0;

template<typename T>
std::string tostring(T val)
{
	std::stringstream ss;
	std::string str;
	ss<<val;
	ss>>str;
	return str;
}

template<typename T>
T Add(T a,T b)
{
	return a+b;
}

template<typename T>
T convert(const char *str)
{
	std::stringstream ss(str);
	T t;
	ss >> t;
	return t;
}

void tobytes(std::string str);

void InitPath(std::string stream,std::string cache);

uint xhash(const char* ch);

void split(const std::string& s, std::vector<std::string>& v, const std::string& c);

int new_id();

bool isNumber(const std::string& value);

int countUTF8Char(const std::string &s);

std::string readFile(const char* file);

#endif