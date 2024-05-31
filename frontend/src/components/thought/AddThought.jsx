import React, { useState, useEffect } from "react";
import _ from "lodash";

import Select from "@mui/material/Select";
import Grid from "@mui/material/Unstable_Grid2";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import FormHelperText from "@mui/material/FormHelperText";
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
  getThoughtCategories,
  insertThought,
  insertThoughtCategory,
} from "../../services/ThoughtsService.ts";

export default function AddThought() {
  const [thought, setThought] = useState({
    description: "",
    thoughtCategoryId: 0,
    details: new Array(""),
    jsonDetails: { keys: new Array(""), values: [{ Column1: ""}]},
    websiteLinks: new Array(""),
    textType: "PlainText"
  });
  const [thoughtCategories, setThoughtCategories] = useState([]);  
  const [tabValue, setTabValue] = useState(0);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    getThoughtCategories().then((response) => {
      setThoughtCategories(response.data);
      setIsLoading(false);
    });
  };

  const handleInputChange = (e) => {
    let newState = { ...thought };
    const { name, value } = e.target;
    _.set(newState, name, value);
    setThought(newState);
  };

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
      thought.jsonDetails.json = JSON.stringify(thought.jsonDetails.values);
      var newThought = {
        ...thought,
        details: removeEmptyRows(thought.details),
        websiteLinks: removeEmptyRows(thought.websiteLinks),
      }
      insertThought(newThought).then((response) => {
        alert("Thought added");
        setThought({
          description: "",
          thoughtCategoryId: 0,
          details: [],
          websiteLinks: [],
        });
      });
    }
  };

  const isValid = () => {
    var msg = "";

    if (thought.description.length <= 0) {
      msg += "Description required. ";
    }

    if (thought.thoughtCategoryId <= 0) {
      msg += "Category required. ";
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
    console.log(thought.jsonDetails.values);
    thought.jsonDetails.values.forEach(v => {
      v[`Column${thought.jsonDetails.keys.length}`] = "";
    });
    let newState = { ...thought };
    setThought(newState);
  };

  const addRow = () => {
    var newRow = {};
    thought.jsonDetails.keys.forEach((k, i) => {
      newRow[`Column${i}`] = "";
    });
    thought.jsonDetails.values.push({});
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

  const addThoughtCategory = (description) => {
    var thoughtCategory = {
      description: description,
    };
    insertThoughtCategory(thoughtCategory).then(loadCategories);
  };

  const handleTabChange = (event, newValue) => {
    setTabValue(newValue);
  };

  return (
    <Grid container spacing={2} className="m-2">
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
        <Grid xs={4}>
          <FormControl fullWidth>
            <InputLabel id="thought-category-select-label">
              Category *
            </InputLabel>
            <Select
              labelId="thought-category-select-label"
              id="thought-category-select"
              name="thoughtCategoryId"
              label="Category"
              maxWidth
              value={thought.thoughtCategoryId}
              onChange={handleInputChange}
            >
              <MenuItem value="0">
                <em>None</em>
              </MenuItem>
              {thoughtCategories.map((tc, i) => (
                <MenuItem key={i} value={tc.id}>
                  {tc.description}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid xs={12}>
          {/* <AddItemToDropdownDialog isOpen={false} title="Category" saveCallback={addThoughtCategory}></AddItemToDropdownDialog> */}
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
              handleAddRow={addRow}
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
        </Grid>
    </Grid>
  );
}
