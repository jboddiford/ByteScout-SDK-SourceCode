//*******************************************************************************************//
//                                                                                           //
// Download Free Evaluation Version From: https://bytescout.com/download/web-installer       //
//                                                                                           //
// Also available as Web API! Free Trial Sign Up: https://secure.bytescout.com/users/sign_up //
//                                                                                           //
// Copyright © 2017-2018 ByteScout Inc. All rights reserved.                                 //
// http://www.bytescout.com                                                                  //
//                                                                                           //
//*******************************************************************************************//


using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace ByteScoutWebApiExample
{
	class Program
	{
		// The authentication key (API Key).
		// Get your own by registering at https://app.pdf.co/documentation/api
		const String API_KEY = "**************************************";

        // Direct URL of source PDF file.
        const string SourceFileUrl = "https://s3-us-west-2.amazonaws.com/bytescout-com/files/demo-files/cloud-api/pdf-to-csv/sample.pdf";
		// Comma-separated list of page indices (or ranges) to process. Leave empty for all pages. Example: '0,2-5,7-'.
		const string Pages = "";
		// PDF document password. Leave empty for unprotected documents.
		const string Password = "";
		// Destination CSV file name
		const string DestinationFile = @".\result.csv";

        // Sample profile that sets advanced conversion options
        // Advanced options are properties of CSVExtractor class from ByteScout PDF Extractor SDK used in the back-end:
        // https://cdn.bytescout.com/help/BytescoutPDFExtractorSDK/html/87ce5fa6-3143-167d-abbd-bc7b5e160fe5.htm

        static string Profiles = File.ReadAllText("profile.json");

		static void Main(string[] args)
		{
			// Create standard .NET web client instance
			WebClient webClient = new WebClient();

			// Set API Key
			webClient.Headers.Add("x-api-key", API_KEY);

			// Prepare URL for `PDF To CSV` API call
			string query = Uri.EscapeUriString(string.Format(
                "https://api.pdf.co/v1/pdf/convert/to/csv?name={0}&password={1}&pages={2}&url={3}&profiles={4}", 
				Path.GetFileName(DestinationFile),
				Password,
				Pages,
				SourceFileUrl,
                Profiles));

			try
			{
				// Execute request
				string response = webClient.DownloadString(query);

				// Parse JSON response
				JObject json = JObject.Parse(response);

                if (json["status"].ToString() != "error")
                {
					// Get URL of generated CSV file
					string resultFileUrl = json["url"].ToString();

					// Download CSV file
					webClient.DownloadFile(resultFileUrl, DestinationFile);

					Console.WriteLine("Generated CSV file saved as \"{0}\" file.", DestinationFile);
				}
				else
				{
					Console.WriteLine(json["message"].ToString());
				}
			}
			catch (WebException e)
			{
				Console.WriteLine(e.ToString());
			}

			webClient.Dispose();


			Console.WriteLine();
			Console.WriteLine("Press any key...");
			Console.ReadKey();
		}
	}
}
