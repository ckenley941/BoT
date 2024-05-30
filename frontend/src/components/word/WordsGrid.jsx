import React, { useState, useEffect } from "react"

import Grid from "@mui/material/Unstable_Grid2";
import { DataGrid, GridToolbar   } from '@mui/x-data-grid';

import { getWords, getWordById } from "../../services/WordsService.ts";
import WordCard from "./WordCard.jsx";

import WordDialog from "./WordDialog.jsx";

import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';

export default function WordsGrid() {
    const [words, setWords] = useState([]);
    const [open, setOpen] = React.useState(false);
    const [selectedWord, setSelectedWord] = useState({ id: 0,
      word: "",
      primaryTranslation: {
        id: 0,
        word: "",
      },
      pronunication:[]});

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
      },[selectedWord]);
    
      const loadData = async () => {
        getWords().then((response) => {
            setWords(response.data);
        });
      };

      const handleRowClick = (params) => {
        getWordById(params.id).then((response) => {
          setSelectedWord(response.data);
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
            onRowClick={handleRowClick} {...words} 
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
        {/* <WordDialog isOpen={open} selectedWord={selectedWord}></WordDialog> */}
         <Dialog open={open} onClose={handleClose} fullWidth={true} maxWidth={true}>
         <DialogTitle>Word</DialogTitle>
         <DialogContent>
             <WordCard data={selectedWord}></WordCard>            
         </DialogContent>
         <DialogActions>
           <Button onClick={handleClose}>Close</Button>
         </DialogActions>
       </Dialog>
       </Grid>
      );
}