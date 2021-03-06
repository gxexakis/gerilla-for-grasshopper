﻿'Import the GH stuff

Imports Grasshopper.Kernel

Imports Grasshopper.Kernel.Types

Imports Rhino.Geometry

Imports System 

Imports System.IO

Imports System.Diagnostics

Public Class GerillaAssemblyLibrary
Inherits GH_Component
    
    'Constructor
    Public Sub New()
    	
    	MyBase.New("Gerilla Assembly Library", "Gerilla Assembly", "Select Assembly From Gerilla Assembly Library", "Gerilla", "Materials")
    	
    End Sub
    
    
    
    Public Overrides ReadOnly Property ComponentGuid As System.Guid

        Get
 		Return New Guid("5DD5B677-EA1C-4CD0-8594-AC99F8795427") 'www.createguid.com
        End Get

    End Property
    
    	Protected Overrides ReadOnly property icon As System.drawing.Bitmap
 		Get
    		Return gerillaResource.GerillaIconLibrary  '''.MakeTransparent(System.Drawing.Color.White)
 	    End Get
		End Property
    
    
    Protected Overrides Sub RegisterInputParams(ByVal pManager As Grasshopper.Kernel.GH_Component.GH_InputParamManager)

		pManager.AddIntegerParameter("Select Gerilla Assembly", "Select Assembly", "Select Assembly From Gerilla Assembly Library", GH_ParamAccess.list, 0)
		
    End Sub


    Protected Overrides Sub RegisterOutputParams(ByVal pManager As Grasshopper.Kernel.GH_Component.GH_OutputParamManager)
    	
    	pManager.AddTextParameter("Gerilla Assembly IDF", "IDF", "Gerilla Assembly For IDF Compiler", GH_ParamAccess.list)
    	pManager.AddTextParameter("Gerilla Assembly Name","Assembly Name","Gerilla Assembly Name For Application To Gerilla Surface", GH_ParamAccess.list)
    End Sub
    
    
    Protected Overrides Sub SolveInstance(ByVal DA As Grasshopper.Kernel.IGH_DataAccess)
    	    	
    	'Private Variables
    	
    	Dim GerillaAssemblyCount As Integer = Nothing
    	Dim GerillaAssemblyList As New ArrayList
    	Dim SelectedAssemblyNumber As New List(Of Integer) ' = Nothing
    	Dim AssemblyChoice As StreamReader 
    	Dim GerillaFilePath As String = ("C:\geRILLA\Libraries\Assemblies\")
    	Dim GerillaAssemblyName As New List(Of String)
		If (Not DA.GetDataList(0,SelectedAssemblyNumber)) Then Return
		
		'Find out how many files are in the Project assembly Library
    	GerillaAssemblyCount = System.IO.Directory.GetFiles(GerillaFilePath).Length()
'    	MsgBox(ProjectassemblyCount)
'		MsgBox(ProjectFilePath)

		Dim FullassemblyPath As String 
		Dim assemblyFileName As String
		Dim assemblyName As String 

		Dim assemblyOut As New Grasshopper.DataTree(Of String)
		'Get the list of assembly Names
		
		For i As Integer = 0 To GerillaassemblyCount - 1
    		
    		FullassemblyPath = System.IO.Directory.GetFiles(GerillaFilePath)(i)
    		
    		'assemblyFileName = Replace(FullassemblyPath, GerillaFilePath, "")
    		'assemblyName = Replace(assemblyFileName, ".txt", "")
    		
    		'GerillaassemblyList.Add(assemblyName)
    		GerillaassemblyList.Add(FullassemblyPath)
    		
    	Next
    	
    	For i As Integer = 0 To SelectedAssemblyNumber.Count-1
    		Dim assemblyPath As New Grasshopper.Kernel.Data.GH_Path(i)
    	
	    	If SelectedassemblyNumber(i) < GerillaAssemblyCount Then
	    		
	    		'assemblyChoice = GerillaAssemblyList(SelectedAssemblyNumber)
				Dim assemblyLine As String = nothing
	    		AssemblyChoice = System.IO.File.OpenText(GerillaAssemblyList(SelectedAssemblyNumber(i)))
	    		Do While AssemblyChoice.Peek()>= 0 
	    			
	    			assemblyLine = AssemblyChoice.ReadLine
	    			assemblyOut.Add(assemblyLine, assemblyPath)
	    			Loop
	    	Else
	    		
	    		'assemblyChoice = "ERROR: No assembly choosen." 
	    		
	    		assemblyOut.Add("ERROR: No assembly choosen.", assemblyPath)
	    		MsgBox("ERROR: No Assembly choosen. Choose a number that is within the bounds of the Gerilla Assembly Library.")
	    		
	    	End If
    	Next
    	
    	For i As Integer = 0 To assemblyOut.BranchCount-1
    		GerillaAssemblyName.Add(assemblyOut.Branch(i).Item(1))
    	Next
    	
    	
'    	'Write the assembly choice to the Gerilla assemblys List
'    	AllProjectassemblys.PublicAllGerillaassemblysList.Add(assemblyChoice)
'    	
'    	Dim InstanceGuid As Guid
'    	
'    	MsgBox(InstanceGuid.ToString)
    	
'    	Dim GerillaassemblyDictionary As GerillaDictionary(InstanceGuid.ToString, assemblyChoice)
    	
		'Assign Outputs
		DA.SetDataTree(0, assemblyOut)
		DA.SetDataList(1, GerillaAssemblyName)

    End Sub
    
End Class