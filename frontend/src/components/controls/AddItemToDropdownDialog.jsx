import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import IconButton from "@mui/material/IconButton";
import Tooltip from '@mui/material/Tooltip';

import AddIcon from '@mui/icons-material/Add';

export default function AddItemToDropdownDialog({ isOpen, title, saveCallback }) {
  const [open, setOpen] = React.useState(isOpen);
  const [description, setDescription] = React.useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleInputChange = (e) => {
    setDescription(e.target.value);
  }

  const handleSave = () => {    
    if (description.length > 0){
        saveCallback(description);
        handleClose();
    }
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Tooltip title={`Add ${title}`}>      
          <IconButton color="secondary" aria-label="Add" onClick={handleClickOpen}>
            <AddIcon />
          </IconButton>   
        </Tooltip> 
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Add {title}</DialogTitle>
        <DialogContent>
            <TextField
            autoFocus
            margin="dense"
            label={title}
            fullWidth
            variant="standard"
            onChange={handleInputChange}
          />                
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSave}>Save</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}