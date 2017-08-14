# Pdf Parser
Pdf parser for the parking area of Digipolis.

This repository contains 3 projects: 

 - **PDFParser_Core** : Class library containing all logic for parsing to json and csv
 - **PDFParser** : Executable Command line application
 - **DpParkingsParser** : Asp.net API that handles incoming mails using the mailgun API
 
## PDFParser

### Usage

```shel
PDFParser.exe inputfile [-o outputfile] [-f outputformat(CSV/JSON)]
```

## DpParkingsParser

### Requirements

 - ASP.net compatible server
 - Microsoft Azure Blob service
 - Mailgun Api Subscription
 
### Installation

**Web.config**
  - Edit Appsettings keys to include your Azure blob connection string and mailgun credentials
  - Rename Web.config.example to Web.config

 **Mailgun Configuration**
 
  - See [mailgun docs](http://mailgun-documentation.readthedocs.io/en/latest/quickstart-receiving.html) to setup a working incomming mail address
  - Configure a **Store and notify** route to the application webhook at <Site-Root>/api/mail

