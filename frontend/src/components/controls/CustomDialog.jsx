import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import IconButton from "@mui/material/IconButton";

import AddIcon from '@mui/icons-material/Add';

//TODO - pass in dialog object with different properties - for now specializing to pronunciation
export default function CustomDialog({ isOpen }) {
  const [open, setOpen] = React.useState(isOpen);
  const [phonetics, setPhonetics] = React.useState([""]);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleSave = () => {
    handleClose();
  };

  const handleClose = () => {
    setOpen(false);
  };

  const addPhonetic = () => {
    console.log(phonetics);
    setPhonetics( // Replace the state
        [ // with a new array
        ...phonetics, // that contains all the old items
        "" // and one new item at the end
        ])
  };

  return (
    <div>
      {/* <Button variant="outlined" onClick={handleClickOpen}>
        Open form dialog
      </Button> */}
          <IconButton color="secondary" aria-label="Add" onClick={handleClickOpen}>
            <AddIcon />
          </IconButton>    
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Pronunication</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Add pronunciation
          </DialogContentText>
          {
            phonetics.map((p, i, {length}) =>
            <TextField
            autoFocus
            margin="dense"
            label="Phonetic"
            fullWidth
            variant="standard"
          />
          )
            }       
                <IconButton color="secondary" aria-label="Add" onClick={addPhonetic}>
          <AddIcon />
        </IconButton>     
       
            
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSave}>Save</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}