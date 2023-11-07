import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { DataGrid, GridToolbar  } from '@mui/x-data-grid';

import { getThoughtCategories } from "../../services/ThoughtsService.ts";

export default function ThoughtsCategoriesGrid() {
    const [thoughtCategories, setThoughtCategories] = useState([]);

    const columns = [
        //{ field: 'id', headerName: 'ID', width: 90 },
        {
          field: 'description',
          headerName: 'Description',
          width: 150,
          editable: false,
        },
        {
          field: 'parentCategory',
          headerName: 'Parent Category',
          width: 150,
          editable: false,
        },
        {
            field: 'sortOrder',
            headerName: 'Sort Order',
            width: 200,
            editable: false,
          }
      ];

    useEffect(() => {
        loadData();
      }, []);
    
      const loadData = async () => {
        getThoughtCategories().then((response) => {
          setThoughtCategories(response.data);
        });
      };

      //https://mui.com/x/react-data-grid/
      return (
        <Card sx={{ height: 800, width: '100%' }}>
            <CardContent>
          <DataGrid
            //getRowId={(row) => row.thoughtId}
            rows={thoughtCategories}
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