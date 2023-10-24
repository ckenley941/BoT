import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { DataGrid, GridToolbar  } from '@mui/x-data-grid';

import { getThoughts } from "../../services/ThoughtsService.ts";

export default function ThoughtsGrid() {
    const [thoughts, setThoughts] = useState([]);

    const columns = [
        //{ field: 'id', headerName: 'ID', width: 90 },
        {
          field: 'description',
          headerName: 'Description',
          width: 150,
          editable: false,
        },
        {
          field: 'category',
          headerName: 'Category',
          width: 150,
          editable: false,
        },
        {
            field: 'detailsLimited',
            headerName: 'Details',
            width: 500,
            editable: false,
          }
      ];

    useEffect(() => {
        loadData();
      }, []);
    
      const loadData = async () => {
        getThoughts().then((response) => {
            setThoughts(response.data);
        });
      };

      //https://mui.com/x/react-data-grid/
      return (
        <Card sx={{ height: 800, width: '100%' }}>
            <CardContent>
          <DataGrid
            //getRowId={(row) => row.thoughtId}
            rows={thoughts}
            columns={columns}
            initialState={{
              pagination: {
                paginationModel: {
                  pageSize: 5,
                },
              },
            }}
            pageSizeOptions={[5]}
            checkboxSelection
            disableRowSelectionOnClick
            slots={{ toolbar: GridToolbar }}
            slotProps={{
              toolbar: {
                showQuickFilter: true,
              },
            }}
          />
          </CardContent>
        </Card>
      );
}