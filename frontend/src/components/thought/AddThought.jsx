import React, { useState, useEffect } from "react";
import _ from "lodash";

import Select from "@mui/material/Select";
import Grid from "@mui/material/Unstable_Grid2";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import TabPanel from "../controls/TabPanel.jsx";

import IconButton from "@mui/material/IconButton";
import SaveIcon from "@mui/icons-material/Save";
import AddItemToDropdownDialog from "../controls/AddItemToDropdownDialog";
import TextField from "@mui/material/TextField";

import DetailRow from "../controls/DetailRow";

import {
  getThoughtBuckets,
  insertThought,
} from "../../services/ThoughtsService.ts";
import AddOutdoorActivity from "../outdoors/AddOutdoorActivity.jsx";

export default function AddThought() {
  const [thought, setThought] = useState({
    description: "",
    thoughtBucketId: 1,
    details: new Array(""),
    jsonDetails: { keys: new Array(""), values: [{ Column1: ""}]},
    websiteLinks: new Array(""),
    textType: "PlainText"
  });
  const [thoughtBuckets, setThoughtBuckets] = useState([]);  
  const [tabValue, setTabValue] = useState(0);
  const [isLoading, setIsLoading] = useState(true);
  const [selectedThoughtType, setSelectedThoughtType]= useState("Thought");

  useEffect(() => {
    loadBuckets();
  }, []);

  const loadBuckets = async () => {
    getThoughtBuckets().then((response) => {
      setThoughtBuckets(response.data);
      setIsLoading(false);
    });
  };

  const handleInputChange = (e) => {
    let newState = { ...thought };
    const { name, value } = e.target;
    _.set(newState, name, value);
    setThought(newState);
  };

  const handleSelectedThoughtTypeChange = (e) => {
    setSelectedThoughtType(e.target.value);
  }

  const handleDetailChange = (e) => {
    var index = parseInt(e.target.name);
    thought.details[index] = e.target.value;
    let newState = { ...thought };
    setThought(newState);
  };

  const handleJsonColumnChange = (e) => {
    var index = parseInt(e.target.name);
    thought.jsonDetails.keys[index] = e.target.value;
    let newState = { ...thought };
    setThought(newState);
  };

  const handleJsonRowChange = (e) => {
    var colIndex = parseInt(e.target.name.split("|")[0]) + 1;
    var rowIndex = parseInt(e.target.name.split("|")[1]);
    thought.jsonDetails.values[rowIndex][`Column${colIndex}`] = e.target.value;
    let newState = { ...thought };
    setThought(newState);
  };

  const handleWebsiteLinkChange = (e) => {
    var index = parseInt(e.target.name);
    thought.websiteLinks[index] = e.target.value;
    let newState = { ...thought };
    setThought(newState);
  };

  const addThought = () => {
    if (isValid()) {
      if (thought.textType === "Json"){
        thought.jsonDetails.json = JSON.stringify(thought.jsonDetails.values);
      }
      var newThought = {
        ...thought,
        details: removeEmptyRows(thought.details),
        websiteLinks: removeEmptyRows(thought.websiteLinks),
      }
      insertThought(newThought).then((response) => {
        alert("Thought added");
        setThought({
          description: "",
          thoughtBucketId: 1,
          details: new Array(""),
          jsonDetails: { keys: new Array(""), values: [{ Column1: ""}]},
          websiteLinks: new Array(""),
          textType: "PlainText"
        });
      });
    }
  };

  const isValid = () => {
    var msg = "";

    if (thought.description.length <= 0) {
      msg += "Description required. ";
    }

    if (msg.length > 0) {
      alert(msg);
      return false;
    }

    return true;
  };

  const removeEmptyRows = (rows) => {
    return _.filter( rows, r => r !== "" );
  }

  const addDetail = () => {
    thought.details.unshift("");
    let newState = { ...thought };
    setThought(newState);
  };

  const addColumn = () => {
    thought.jsonDetails.keys.push("");
    thought.jsonDetails.values.forEach(v => {
      v[`Column${thought.jsonDetails.keys.length}`] = "";
    });
    let newState = { ...thought };
    setThought(newState);
  };

  const deleteColumn = (index) => {
    if (index > 0){
      thought.jsonDetails.keys.splice(1, index);
    }
    else if (thought.jsonDetails.keys.length > 1){
      thought.jsonDetails.keys.splice(index, 1);
    }

    thought.jsonDetails.values.forEach(v => {
      let columns = Object.getOwnPropertyNames(v);
      columns.forEach(c => {
        var colName = parseInt(c.replace("Column", ""));
        if (colName === index + 1){
          delete v[`Column${colName}`];
        }
        else if (colName > index + 1){
          v[`Column${colName - 1}`] = v[`Column${colName}`]; 
          delete v[`Column${colName}`];
        }
      });
    });    
    let newState = { ...thought };
    setThought(newState);
  };

  const addRow = () => {
    let newRow = {};
    thought.jsonDetails.keys.forEach((k, i) => {
      newRow[`Column${i + 1}`] = "";
    });
    thought.jsonDetails.values.push(newRow);
    let newState = { ...thought };
    setThought(newState);
  };

  const deleteRow = (index) => {
    if (index > 0){
      thought.jsonDetails.values.splice(1, index);
    }
    else if (thought.jsonDetails.values.length > 1){
      thought.jsonDetails.values.splice(index, 1);
    }
    let newState = { ...thought };
    setThought(newState);
  };

  const addWebsiteLink = () => {
    thought.websiteLinks.unshift("");
    let newState = { ...thought };
    setThought(newState);
  };

  const deleteDetail = (e) => {
    var index = parseInt(e.currentTarget.name);
    delete thought.details[index];
    let newState = { ...thought };
    setThought(newState);
  };

  const deleteWebsiteLink = (e) => {
    var index = parseInt(e.currentTarget.name);
    delete thought.websiteLinks[index];
    let newState = { ...thought };
    setThought(newState);
  };

  // const addThoughtBucket = (description) => {
  //   var thoughtBucket = {
  //     description: description,
  //   };
  //   insertThoughtBuckety(thoughtBucket).then(loadBuckets);
  // };

  const handleTabChange = (event, newValue) => {
    setTabValue(newValue);
  };

  return (
    <Grid container spacing={2} className="m-2">
       <Grid xs={12}>
          <FormControl sx={{width:"50%"}}>
            <InputLabel id="thought-bucket-select-label">
              Bucket
            </InputLabel>
            <Select
              labelId="thought-bucket-select-label"
              id="thought-bucket-select"
              name="thoughtBucketId"
              label="Bucket"
              maxWidth
              value={thought.thoughtBucketId}
              onChange={handleInputChange}
            >
              {thoughtBuckets.map((tc, i) => (
                <MenuItem key={i} value={tc.id}>
                  {tc.description}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          {thought.thoughtBucketId === 2 && (
          <FormControl sx={{width:"20%"}} className="ml-2">
          <Select              
              value={selectedThoughtType}
              onChange={handleSelectedThoughtTypeChange}
            >
              <MenuItem key={"Thought"} value={"Thought"}>
                Thought
                </MenuItem>
                <MenuItem key={"OutdoorActivity"} value={"OutdoorActivity"}>
                Outdoor Activity
                </MenuItem>
               </Select>
          </FormControl>)}
        </Grid>       
       
        
        {selectedThoughtType === "Thought" && (
          <>
           <Grid xs={10}>
           <TextField
             name="description"
             value={thought.description}
             label="Thought"
             onChange={handleInputChange}
             fullWidth
             multiline
           />
         </Grid>
        <Grid xs={12}>
          {/* <AddItemToDropdownDialog isOpen={false} title="Bucket" saveCallback={addThoughtBucket}></AddItemToDropdownDialog> */}
          <Tabs
            value={tabValue}
            onChange={handleTabChange}
            textColor="secondary"
            indicatorColor="secondary"
            aria-label="secondary tabs example"
          >
            <Tab label="Details" />
            <Tab label="Related Thoughts" />
            <Tab label="Website Links" />
          </Tabs>

          <TabPanel value={tabValue} index={0}>
            <DetailRow
              title="Detail"
              textData={thought.details}
              jsonData={thought.jsonDetails}
              selectedTextType={thought.textType}
              handleTextTypeChange={handleInputChange}
              handleInputChange={handleDetailChange}
              handleJsonColumnChange={handleJsonColumnChange}
              handleJsonRowChange={handleJsonRowChange}
              handleAddDetail={addDetail}
              handleAddColumn={addColumn}
              handleDeleteColumn={deleteColumn}
              handleAddRow={addRow}
              handleDeleteRow={deleteRow}
              handleDelete={deleteDetail}
            ></DetailRow>
          </TabPanel>
          <TabPanel value={tabValue} index={1}>
            Under construction
          </TabPanel>
          <TabPanel value={tabValue} index={2}>
            <DetailRow
              title="Link"
              textData={thought.websiteLinks}
              selectedTextType={"PlainText"}
              handleAdd={addWebsiteLink}
              handleInputChange={handleWebsiteLinkChange}
              handleDelete={deleteWebsiteLink}
            ></DetailRow>
          </TabPanel>
        <div className="ml-3">
          <IconButton
            color="secondary"
            aria-label="Save Thought"
            onClick={addThought}
          >
            <Tooltip title="Save Thought">
              <SaveIcon fontSize="large" />
            </Tooltip>
          </IconButton></div>
        </Grid> </>)}
        {selectedThoughtType === "OutdoorActivity" && (
          <Grid xs={12}>
            <AddOutdoorActivity></AddOutdoorActivity>
          </Grid>
        )}
    </Grid>
  );
}
