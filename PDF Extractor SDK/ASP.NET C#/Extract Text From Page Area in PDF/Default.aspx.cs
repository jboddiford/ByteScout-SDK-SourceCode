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
using System.Drawing;
using Bytescout.PDFExtractor;

namespace ExtractTextFromPageArea
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			String inputFile = Server.MapPath(@".\bin\columns.pdf");

			// Create Bytescout.PDFExtractor.TextExtractor instance
			TextExtractor extractor = new TextExtractor();
			extractor.RegistrationName = "demo";
			extractor.RegistrationKey = "demo";
			
			// Load sample PDF document
			extractor.LoadDocumentFromFile(inputFile);

			// Get dimensions of the first document page
			RectangleF rectangle = extractor.GetPageRectangle(0);

			// Get text from the 1/3 of the page

			rectangle.Width = rectangle.Width / 3f;
			
			Response.Clear();
			Response.ContentType = "text/html";

			extractor.SetExtractionArea(rectangle);

			Response.Write("<pre>");

			// Save extracted text to output stream
			extractor.SavePageTextToStream(0, Response.OutputStream);

			Response.Write("</pre>");
			
			Response.End();
		}
	}
}
