import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { DataGrid, GridToolbar  } from '@mui/x-data-grid';

import { getWords } from "../../services/WordsService.ts";

export default function WordsGrid() {
    const [words, setWords] = useState([]);

    const columns = [
        //{ field: 'id', headerName: 'ID', width: 90 },
        {
          field: 'word1',
          headerName: 'Spanish Word',
          width: 150,
          editable: false,
        },
        {
          field: 'word2',
          headerName: 'English Word',
          width: 150,
          editable: false,
        },
        {
            field: 'word1Example',
            headerName: 'Spanish Example',
            width: 500,
            editable: false,
          },
          {
            field: 'word2Example',
            headerName: 'English Translation',
            width: 500,
            editable: false,
          }
      ];

    useEffect(() => {
        loadData();
      }, []);
    
      const loadData = async () => {
        getWords().then((response) => {
            setWords(response.data);
        });
      };

      //https://mui.com/x/react-data-grid/
      return (
        <Card sx={{ height: 1200, width: '100%' }}>
            <CardContent>
          <DataGrid
            getRowId={(row) => row.word1Id}
            rows={words}
            columns={columns}
            initialState={{
              pagination: {
                paginationModel: {
                  pageSize: 10,
                },
              },
            }}
            pageSizeOptions={[10]}
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