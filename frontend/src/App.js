import './App.css';
import { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import React from 'react';
import Clock from './Clock.jsx'

import Checkbox from '@mui/material/Checkbox';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import AppBar from '@mui/material/AppBar';
import FormControlLabel from '@mui/material/FormControlLabel';

import SaveIcon from '@mui/icons-material/Save'
import DeleteIcon from '@mui/icons-material/Delete'


import WordCard from './components/word/WordCard';
import Thought from './components/thought/Thought'
import AddThought from './components/thought/AddThought'
import Layout from './components/layout/Layout'

function Test(){
  return (
<h1>Test</h1>
  )
};

function CheckBoxExample(){
  const [checked, setChecked] = React.useState(true);
  return (<div>
    <FormControlLabel label="Testing"
    control={ <Checkbox 
    checked={checked} 
    icon={<DeleteIcon></DeleteIcon>}
    checkedIcon={<SaveIcon></SaveIcon>}
    onChange = {(e) => setChecked(e.target.checked)}
    inputProps={{'aria-label' : 'secondary-checkbox'}}
  ></Checkbox>}></FormControlLabel>
   </div>
  );
}

const App = () => {
  return (
    <Router>
      <Layout>
        <Routes>
        <Route exact path="/" element={ <WordCard/> }>
        </Route>
        <Route exact path="/thought"  element={ <Thought/>}>
        </Route>
        <Route exact path="/add-thought"  element={ <AddThought/>}>
        </Route>
        </Routes>
        </Layout>
      </Router>
  );
}

export default App;
