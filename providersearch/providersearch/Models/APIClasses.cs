using System;
using System.Collections.Generic;

// URL Encoding for command is %2

//https://maps.googleapis.com/maps/api/geocode/json?address=mesa,arizona

namespace providersearch.Models.specialities
{
    public class Meta
    {
        public string data_type { get; set; }
        public string item_type { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }

    public class Datum
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string actor { get; set; }
        public string actors { get; set; }
    }

    public class RootObject
    {
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }
}
namespace providersearch.Models.insurance
{
    public class Meta
    {
        public string data_type { get; set; }
        public string item_type { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }

    public class Plan
    {
        public string uid { get; set; }
        public string name { get; set; }
        public List<string> category { get; set; }
    }

    public class Datum
    {
        public string uid { get; set; }
        public string name { get; set; }
        public List<Plan> plans { get; set; }
    }

    public class RootObject
    {
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }
}

namespace providersearch.Models.googlecitysearch
{
	public class AddressComponent
	{
		public string long_name { get; set; }
		public string short_name { get; set; }
		public List<string> types { get; set; }
	}

	public class Northeast
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Southwest
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Bounds
	{
		public Northeast northeast { get; set; }
		public Southwest southwest { get; set; }
	}

	public class Location
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Northeast2
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Southwest2
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Viewport
	{
		public Northeast2 northeast { get; set; }
		public Southwest2 southwest { get; set; }
	}

	public class Geometry
	{
		public Bounds bounds { get; set; }
		public Location location { get; set; }
		public string location_type { get; set; }
		public Viewport viewport { get; set; }
	}

	public class Result
	{
		public List<AddressComponent> address_components { get; set; }
		public string formatted_address { get; set; }
		public Geometry geometry { get; set; }
		public string place_id { get; set; }
		public List<string> types { get; set; }
	}

	public class RootObject
	{
		public List<Result> results { get; set; }
		public string status { get; set; }
	}
}

namespace providersearch.Models.providers
{
    
}
