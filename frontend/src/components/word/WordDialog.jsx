import React, { useState, useEffect } from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import { DataGrid, GridToolbar   } from '@mui/x-data-grid';

import { getWords, getWordById } from "../../services/WordsService.ts";
import WordCard from "./WordCard.jsx";

import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';

export default function WordDialog(isOpen, selectedWord) {
    console.log(selectedWord);
    const [open, setOpen] = React.useState(isOpen);
    
    const handleClose = () => {
      setOpen(false);
    };


      //https://mui.com/x/react-data-grid/
      return (
        <div>      
         <Dialog open={open} onClose={handleClose}>
         <DialogTitle>Word</DialogTitle>
         <DialogContent>
             <WordCard data={selectedWord}></WordCard>            
         </DialogContent>
         <DialogActions>
           <Button onClick={handleClose}>Close</Button>
         </DialogActions>
       </Dialog>
       </div>
      );
}