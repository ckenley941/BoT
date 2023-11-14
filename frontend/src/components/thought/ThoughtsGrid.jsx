import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { DataGrid, GridToolbar  } from '@mui/x-data-grid';

import Thought from "./Thought.jsx";
import { getThoughts, getThoughtById } from "../../services/ThoughtsService.ts";

import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';

export default function ThoughtsGrid() {
    const [thoughts, setThoughts] = useState([]);
    const [open, setOpen] = React.useState(false);
    const [selectedRow, setSelectedRow] = useState({ id: 0,
      guid: "",
      word: "",
      primaryTranslation: {
        id: 0,
        guid: "",
        word: "",
      },
      pronunication:[]});

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

      const handleRowClick = (params) => {
        getThoughtById(params.id).then((response) => {
          setSelectedRow(response.data);
          setOpen(true);
      });
    };

      const handleClose = () => {
        setOpen(false);
      };  

      //https://mui.com/x/react-data-grid/
      return (
        <div>
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
            onRowClick={handleRowClick} {...thoughts} 
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
         <Dialog open={open} onClose={handleClose} fullWidth={true}>
         <DialogTitle>Thought</DialogTitle>
         <DialogContent>
             <Thought data={selectedRow}></Thought>            
         </DialogContent>
         <DialogActions>
           <Button onClick={handleClose}>Close</Button>
         </DialogActions>
       </Dialog>
       </div>
      );
}