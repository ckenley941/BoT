import React, { useState, useEffect } from "react";
import Grid from "@mui/material/Unstable_Grid2";
import { DataGrid, GridRowModes,
  GridToolbarContainer,
  GridActionsCellItem,
  GridRowEditStopReasons,  } from '@mui/x-data-grid';

import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/DeleteOutlined';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Close';
import { useNavigate } from "react-router-dom";

import { getOutdoorActivityLogs } from "../../services/OutdoorsService.ts";

export default function OutdoorActivityGrid(props) {
    const [rows, setRows] = useState([]);
    const navigate = useNavigate()

    const [rowModesModel, setRowModesModel] = React.useState({});

    function AddToolbar(props) {    
      const handleAdd = () => {
        navigate("/add-outdoor-activity");
      };
    
      return (
        <GridToolbarContainer>
          <Button color="primary" startIcon={<AddIcon />} onClick={handleAdd}>
            Add Outdoor Activity
          </Button>
        </GridToolbarContainer>
      );
    }

    const columns = [
        {
            field: 'activityDate',
            headerName: 'Date',
            width: 150
        },  
        {
          field: 'activityType',
          headerName: 'Type',
          width: 150
        },
        {
            field: 'activityName',
            headerName: 'Name',
            width: 350
        },      
        {
            field: 'geographicArea',
            headerName: 'Geographica Area',
            width: 200
          }, 
        {
            field: 'activityLength',
            headerName: 'Length',
            width: 125,
            type: "number"
          },
          {
            field: 'elevationGain',
            headerName: 'Gain',
            width: 150,
            type: "number"
          },
          {
            field: 'totalTimeHours',
            headerName: 'Total Hours',
            width: 100,
            type: "number"
          },
          {
            field: 'totalTimeMinutes',
            headerName: 'Total Minutes',
            width: 100,
            type: "number"
          },
          {
            field: 'movingTimeHours',
            headerName: 'Moving Hours',
            width: 100,
            type: "number"
          },
          {
            field: 'movingTimeMinutes',
            headerName: 'Moving Minutes',
            width: 100,
            type: "number"
          },
      ];

    useEffect(() => {
        loadData();
      }, []);
    
      const loadData = async () => {
        getOutdoorActivityLogs().then((response) => {
          setRows(response.data);
        });
      };

      return (
      <Grid>
          <DataGrid
            rows={rows}
            columns={columns}
            slots={{
              toolbar: AddToolbar,
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
          />
          </Grid>
      );
}