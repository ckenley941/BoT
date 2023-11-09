import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { DataGrid, GridToolbar,   GridRowModes,
  GridToolbarContainer,
  GridActionsCellItem,
  GridRowEditStopReasons,  } from '@mui/x-data-grid';

  import Button from '@mui/material/Button';
  import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/DeleteOutlined';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Close';

import { getThoughtCategories, insertThoughtCategory, updateThoughtCategory, deleteThoughtCategory } from "../../services/ThoughtsService.ts";

export default function ThoughtsCategoriesGrid() {
    const [rows, setRows] = useState([]);

    const [rowModesModel, setRowModesModel] = React.useState({});

    const handleRowEditStop = (params, event) => {
      if (params.reason === GridRowEditStopReasons.rowFocusOut) {
        event.defaultMuiPrevented = true;
      }
    };
  
    const handleEditClick = (id) => () => {
      setRowModesModel({ ...rowModesModel, [id]: { mode: GridRowModes.Edit } });
    };
  
    const handleSaveClick = (id) => () => {
      setRowModesModel({ ...rowModesModel, [id]: { mode: GridRowModes.View } });
    };
  
    const handleDeleteClick = (id) => () => {
      if (window.confirm("Are you sure you want to delete this row?")){
        deleteThoughtCategory(id).then(() => {
          setRows(rows.filter((row) => row.id !== id));
        });
   
      }
    };
  
    const handleCancelClick = (id) => () => {
      setRowModesModel({
        ...rowModesModel,
        [id]: { mode: GridRowModes.View, ignoreModifications: true },
      });
  
      const editedRow = rows.find((row) => row.id === id);
      if (editedRow.isNew) {
        setRows(rows.filter((row) => row.id !== id));
      }
    };
  
    const processRowUpdate = (newRow) => {
      const updatedRow = { ...newRow, isNew: newRow.id === 0 };
      setRows(rows.map((row) => (row.id === newRow.id ? updatedRow : row)));

      if (updatedRow.isNew){
        insertThoughtCategory(updatedRow).then((response) => {
          loadData();
        })
      }
      else{
        updateThoughtCategory(updatedRow, newRow.id);        
        return updatedRow;
      }
    };

    const handleProcessRowUpdateError = () => {
      console.log("Error!");
    }
  
    const handleRowModesModelChange = (newRowModesModel) => {
      setRowModesModel(newRowModesModel);
    };

    function EditToolbar(props) {
      const { setRows, setRowModesModel } = props;
    
      const handleClick = () => {
        var id = 0;
        setRows((oldRows) => [{ id, description: '', parentCategory: '', sortOrder: 1, isNew: true }, ...oldRows]);
        setRowModesModel((oldModel) => ({
          ...oldModel,
          [id]: { mode: GridRowModes.Edit, fieldToFocus: 'description' },
        }));
      };
    
      return (
        <GridToolbarContainer>
          <Button color="primary" startIcon={<AddIcon />} onClick={handleClick}>
            Add record
          </Button>
        </GridToolbarContainer>
      );
    }

    const columns = [
        //{ field: 'id', headerName: 'ID', width: 90 },
        {
          field: 'description',
          headerName: 'Description',
          width: 150,
          editable: true
        },
        {
          field: 'parentId',
          headerName: 'Parent Category',
          width: 150,
          editable: true,
          type: "singleSelect",
          valueOptions: rows,
          getOptionValue: (value) => value.id,
          getOptionLabel: (value) => value.description
        },
        {
            field: 'sortOrder',
            headerName: 'Sort Order',
            width: 200,
            editable: true,
            type: "number"
          },
          {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            cellClassName: 'actions',
            getActions: ({ id }) => {
              const isInEditMode = rowModesModel[id]?.mode === GridRowModes.Edit;
              const currentRow = rows.filter((row) => {if (row.id === id){ return row; }})[0];

              if (isInEditMode) {
                return [
                  <GridActionsCellItem
                    icon={<SaveIcon />}
                    label="Save"
                    sx={{
                      color: 'primary.main',
                    }}
                    onClick={handleSaveClick(id)}
                  />,
                  <GridActionsCellItem
                    icon={<CancelIcon />}
                    label="Cancel"
                    className="textPrimary"
                    onClick={handleCancelClick(id)}
                    color="inherit"
                  />,
                ];
              }
              //For now hard-coding: ThoughtModuleId = 9 is other 
              else if (currentRow.thoughtModuleId !== 9){
                return [
                  <GridActionsCellItem
                    icon={<EditIcon />}
                    label="Edit"
                    className="textPrimary"
                    onClick={handleEditClick(id)}
                    color="inherit"
                  />
                ];
              }
              else{
                return [
                  <GridActionsCellItem
                    icon={<EditIcon />}
                    label="Edit"
                    className="textPrimary"
                    onClick={handleEditClick(id)}
                    color="inherit"
                  />,
                  <GridActionsCellItem
                    icon={<DeleteIcon />}
                    label="Delete"
                    onClick={handleDeleteClick(id)}
                    color="inherit"
                  />,
                ];
              }
      
             
            },
          }
      ];

    useEffect(() => {
        loadData();
      }, []);
    
      const loadData = async () => {
        getThoughtCategories().then((response) => {
          setRows(response.data);
        });
      };

      //https://mui.com/x/react-data-grid/
      return (
        <Card sx={{ height: 800, width: '100%' }}>
            <CardContent>
          <DataGrid
           // getRowId={(row) => row.thoughtCategoryId}
            rows={rows}
            columns={columns}
            editMode="row"
            rowModesModel={rowModesModel}
            onRowModesModelChange={handleRowModesModelChange}
            onRowEditStop={handleRowEditStop}
            processRowUpdate={processRowUpdate}
            onProcessRowUpdateError={handleProcessRowUpdateError}
            slots={{
              toolbar: EditToolbar,
            }}
            slotProps={{
              toolbar: { setRows, setRowModesModel },
            }}
            initialState={{
              pagination: {
                paginationModel: {
                  pageSize: 10,
                },
              },
            }}
            pageSizeOptions={[5, 10, 25]}
            // checkboxSelection
            // disableRowSelectionOnClick
            // slots={{ toolbar: GridToolbar }}
            // slotProps={{
            //   toolbar: {
            //     showQuickFilter: true,
            //   },
            // }}
          />
          </CardContent>
        </Card>
      );
}