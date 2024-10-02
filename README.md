# TechOne-Tech-Test

This project provides an API that converts numbers to words. It includes an HTML web user interface (`TechTest - Front End.html`) to allow the routine to be tested interactively.

## Table of Contents
- [Overview](#overview)
- [Setup Instructions](#setup-instructions)
- [Running the Application](#running-the-application)
- [Testing](#testing)

## Overview
This repository includes all necessary files, including source code, test harnesses, configuration data, and a test plan. The server-side implementation is done in C#.

### Building the Solution
1. Open the solution file `TechOneTest.sln` in Visual Studio.
2. Restore the NuGet packages.
3. Build the solution.

## Running the Application
1. In Visual Studio, set the startup project to `TechOneTest`.
2. Press `F5` to run the application.
3. Open `TechTest - Front End.html` and navigate to `http://localhost:44385/Convert` (Line 43) and change the URL to the according API URL

## Testing
1. Navigate to the `Unit Testing` directory.
2. Open the `NumToWordsControllerTests.cs` file to review the test cases.
3. Run the unit tests using the Test Explorer in Visual Studio.

## Test Plan

### Objective
To verify that the `ConvertToWords` method in the `NumToWordsController` class correctly converts various decimal numbers to their word representations.

### Test Cases

1. **Whole Number**
   - **Input**: 123
   - **Expected Output**: "ONE HUNDRED AND TWENTY-THREE DOLLARS"
   - **Test Method**: `ConvertToWords_ShouldReturnExpectedResult_ForWholeNumber`

2. **Number with Cents**
   - **Input**: 123.45M
   - **Expected Output**: "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS"
   - **Test Method**: `ConvertToWords_ShouldReturnExpectedResult_ForNumberWithCents`

3. **Zero**
   - **Input**: 0
   - **Expected Output**: "ZERO DOLLARS"
   - **Test Method**: `ConvertToWords_ShouldReturnExpectedResult_ForZero`

4. **Large Number**
   - **Input**: 1000000
   - **Expected Output**: "ONE MILLION DOLLARS"
   - **Test Method**: `ConvertToWords_ShouldReturnExpectedResult_ForLargeNumber`

5. **Number with One Cent**
   - **Input**: 1.01M
   - **Expected Output**: "ONE DOLLAR AND ONE CENT"
   - **Test Method**: `ConvertToWords_ShouldReturnExpectedResult_ForNumberWithOneCent`

6. **No Extra Spaces**
   - **Input**: 1000000
   - **Expected Output**: "ONE MILLION DOLLARS"
   - **Test Method**: `ConvertToWords_ShouldNotContainExtraSpaces`


## Approach and Rationale

### Input Validation
Created a `ConversionRequest` model to ensure the passed data is serialized through JSON.

### Separation of Conversion Logic
The core logic of converting the numbers to words is encapsulated within three methods: `ConvertToWords`, `ConvertThreeDigitGroup`, and `ConvertTwoDigitGroup`. This makes the code modular and allows for future potential additions and enhancements.

### Logic
- **ConvertThreeDigitGroup**: Responsible for converting a three-digit group of a number (e.g., thousands, millions) into its word representation. It's utilized by the `ConvertToWords` function to handle the conversion of different segments of a given number.
- **ConvertTwoDigitGroup**: Designed to convert a two-digit group of a number into its word representation. It is used by both the `ConvertThreeDigitGroup` and `ConvertToWords` functions for handling the conversion of tens and teens.
- **ConvertToWords**: Converts the decimal numbers into words.

### Other Possible Approaches
- **Third-Party Libraries**: Using libraries specifically for number-to-word conversion would be a good option, but assuming this is a tech test, it wouldn't be the best approach to showcase my coding and problem-solving skills.
- **Direct String Parsing**: Directly manipulating the strings could be an option to construct the words for each number. This method would be awfully tedious, slow, and prone to errors. It lacks modularity and would be difficult to maintain and scale.
