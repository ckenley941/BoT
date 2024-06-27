import React, { useState, useEffect } from "react";
import Grid from "@mui/material/Unstable_Grid2";
import { DataGrid, GridToolbar  } from '@mui/x-data-grid';

import Thought from "./Thought.jsx";
import { getThoughtsGrid, getThoughtById } from "../../services/ThoughtsService.ts";

import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';

export default function ThoughtsGrid(props) {
    const [thoughts, setThoughts] = useState([]);
    const [open, setOpen] = React.useState(false);
    const [selectedRow, setSelectedRow] = useState({ id: 0,
      word: "",
      primaryTranslation: {
        id: 0,
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
          field: 'bucket',
          headerName: 'Bucket',
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
        if (props.data){
          setThoughts(props.data);
        }
        else{
          getThoughtsGrid().then((response) => {
              setThoughts(response.data);
          });
        }
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
        <Grid>
          <DataGrid
            //getRowId={(row) => row.thoughtId}
            rows={thoughts}
            columns={columns}
            initialState={{
              pagination: {
                paginationModel: {
                  pageSize: 10,
                },               
              },
              filter: {
                filterModel: {
                  items: [],
                  quickFilterValues: [props.globalSearch],
                },
              },
            }}
            onRowClick={handleRowClick} {...thoughts} 
            pageSizeOptions={[10, 25, 50]}
            checkboxSelection
            disableRowSelectionOnClick
            slots={{ toolbar: GridToolbar }}
            
            slotProps={{
              toolbar: {
                showQuickFilter: true,
              },
            }}
          />
         <Dialog open={open} onClose={handleClose} fullWidth={true}>
         <DialogTitle>Thought</DialogTitle>
         <DialogContent>
             <Thought data={selectedRow}></Thought>            
         </DialogContent>
         <DialogActions>
           <Button onClick={handleClose}>Close</Button>
         </DialogActions>
       </Dialog>
       </Grid>
      );
}