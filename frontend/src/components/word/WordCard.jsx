import React, { useState, useEffect } from "react";

import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import Grid from "@mui/material/Unstable_Grid2";
import FormControl from '@mui/material/FormControl';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Switch from '@mui/material/Switch';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import RefreshIcon from '@mui/icons-material/Refresh';
import FlashOnIcon from '@mui/icons-material/FlashOn';
import AddIcon from '@mui/icons-material/Add';

import PropTypes from 'prop-types';


import WordContext from "./WordContext";
import Word from "./Word";
import RelatedWords from "./RelatedWords";
import { getRandomWord, getWordTranslations, getWordTranslationsWithContext, getWordRelationships } from "../../services/WordsService.ts";
import CustomDialog from "../controls/CustomDialog";

const bull = (
  <Box component="span" sx={{ display: "inline-block", mx: "2px", transform: "scale(0.8)" }}>
    •
  </Box>
  );

  function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
  <div role="tabpanel" hidden={value !==index} id={`full-width-tabpanel-${index}`}
    aria-labelledby={`full-width-tab-${index}`} {...other}>
    {value === index && (
    <Box sx={{ p: 3 }}>
      <div>{children}</div>
    </Box>
    )}
  </div>
  );
  }

TabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.number.isRequired,
  value: PropTypes.number.isRequired,
};

export default function WordCard( {data}) {
  let isFlashCard = false;
  const [showFullCard, setShowFullCard] = useState(!isFlashCard); //in flash card mode this is false
  const [isOpen, setIsOpen] = useState(false);
  const [wordCard, setWordCard] = useState({    
      id: 0,
      guid: "",
      word: "",
      primaryTranslation: {
        id: 0,
        guid: "",
        word: "",
      },
      pronunication:[]
  });
  const [translations, setTranslations] = useState([]);
  const [wordContexts, setWordContexts] = useState([]);
  const [wordRelationships, setWordRelationships] = useState([]);

  const [value, setValue] = useState(0);
  const [isDetailedView, setIsDetailedView] = useState(false);

  let wordId = 0;
  useEffect(() => {
    loadData();
  }, [data]);

  const loadData = async () => {
    if (data != null){
      setWordCard(data);
      setChildRecords(data.id);
    }
    else{
      getRandomWord().then((response) => {
        setWordCard(response.data);
        wordId = response.data.id;

        setChildRecords(wordId);
      });
    }
  };

  const setChildRecords= (wordId) =>{
    getWordTranslations(wordId).then((response) => {
      setTranslations(response.data);
    });

    getWordTranslationsWithContext(wordId).then((response) => {
      setWordContexts(response.data);
    });    

    getWordRelationships(wordId).then((response) => {
      setWordRelationships(response.data);
    });    
  }

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  const refreshWord = () => {
    if (isFlashCard){
      setShowFullCard(false);
    }
    loadData();
  };

  return (
  <Box sx={{ minWidth: 275 }}>
    <Card variant="outlined">
      <React.Fragment>
        <CardContent>       
          {/* <IconButton color="secondary" aria-label="Refresh" onClick={refreshWord}>
            <RefreshIcon />
          </IconButton>              */}
          <Typography variant="h2">{wordCard.word} <IconButton color="secondary" aria-label="Flash" onClick={()=> setShowFullCard(!showFullCard)}>
            <FlashOnIcon />
          </IconButton></Typography>
          <Typography variant="h5" component="div">
            {  wordCard.pronunication.map((p, i, {length}) =>
            (i !== length - 1 ?
            (
            <span key={i}>{p}{bull}</span>
            ) :
            (
            <span key={i}>{p}</span>
            )
            ))}
          </Typography>
          {/* {showFullCard ? 
            <span>
              <Typography variant="h6">{wordCard.primaryTranslation ? wordCard.primaryTranslation.word : ""}</Typography> 
              <CustomDialog isOpen={isOpen}></CustomDialog>
            </span>
            : null
          } */}
        </CardContent>
      </React.Fragment>
    </Card>
    {showFullCard ?
      <Card>
        <CardContent>
          <Box sx={{ width: '100%' }}>
            <Tabs value={value} onChange={handleChange} textColor="secondary" indicatorColor="secondary"
              aria-label="secondary tabs example">
              <Tab label="Dictionary" />
              <Tab label="Related Words" />
              <Tab label="Phrases" />
              <Tab label="Synonyms" />
              <Tab label="Antonyms" />
            </Tabs>

            <TabPanel value={value} index={0}>
              <FormControl component="fieldset" variant="standard">
                <FormGroup>
                  <FormControlLabel control={ <Switch checked={isDetailedView} onChange={()=>
                    {setIsDetailedView(!isDetailedView)}} />
                    }
                    label="Detailed View"
                    />
                </FormGroup>
              </FormControl>
              { isDetailedView ?
              ( <Grid>
                {wordContexts.map((wc) => (
                <WordContext data={wc}></WordContext>
                ))}
              </Grid> )
              :
              (
              <Grid container spacing={2}>
                { translations.map((t) => (
                <Grid key={t.id}>
                  <Word word={t.word}></Word>
                </Grid>
                ))}
              </Grid>
              )
              }
            </TabPanel>
            <TabPanel value={value} index={1}>
              <RelatedWords data={wordRelationships.filter(wc=> wc.isRelated === true)}></RelatedWords>
            </TabPanel>
            <TabPanel value={value} index={2}>
              <RelatedWords data={wordRelationships.filter(wc=> wc.isPhrase === true)}></RelatedWords>
            </TabPanel>
            <TabPanel value={value} index={3}>
              <RelatedWords data={wordRelationships.filter(wc=> wc.isSynonym === true)}></RelatedWords>
            </TabPanel>
            <TabPanel value={value} index={4}>
              <RelatedWords data={wordRelationships.filter(wc=> wc.isAntonym === true)}></RelatedWords>
            </TabPanel>
          </Box>
        </CardContent>
      </Card>
      : null
    }
  </Box>
  );
  }
